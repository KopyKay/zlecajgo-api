using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;
using FluentAssertions;
using ZlecajGo.API.IntegrationTests.Fixtures;
using ZlecajGo.API.IntegrationTests.Helpers.TestData;

namespace ZlecajGo.API.IntegrationTests;

public class AuthenticationIntegrationTests : ApiTestBase
{
    [Fact]
    public async Task Register_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var email = $"user_{Guid.NewGuid():N}@test.local";
        var password = "P@ssw0rd1";
        var request = IdentityTestData.CreateRegisterRequest(email, password);

        // Act
        var response = await Client.PostAsJsonAsync("/api/identity/register", request);

        // Assert
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Created);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsTokens()
    {
        // Arrange
        var email = $"user_{Guid.NewGuid():N}@test.local";
        var password = "P@ssw0rd1";
        var registerResponse = await Client.PostAsJsonAsync(
            "/api/identity/register",
            IdentityTestData.CreateRegisterRequest(email, password));

        registerResponse.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Created);

        // Act
        var response =
            await Client.PostAsJsonAsync("/api/identity/login", IdentityTestData.CreateLoginRequest(email, password));
        var payload = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        payload.Should().NotBeNullOrWhiteSpace();

        using var document = JsonDocument.Parse(payload);
        document.RootElement.TryGetProperty("accessToken", out var accessToken).Should().BeTrue();
        document.RootElement.TryGetProperty("refreshToken", out var refreshToken).Should().BeTrue();
        accessToken.GetString().Should().NotBeNullOrWhiteSpace();
        refreshToken.GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetOffers_WithoutToken_ReturnsUnauthorized()
    {
        // Arrange

        // Act
        var response = await Client.GetAsync("/api/offers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetOffers_WithValidToken_ReturnsSuccess()
    {
        // Arrange
        var email = $"user_{Guid.NewGuid():N}@test.local";
        var password = "P@ssw0rd1";
        var registerResponse = await Client.PostAsJsonAsync(
            "/api/identity/register",
            IdentityTestData.CreateRegisterRequest(email, password));

        registerResponse.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Created);

        await SetProfileCompletedAsync(email);

        var loginResponse = await Client.PostAsJsonAsync(
            "/api/identity/login",
            IdentityTestData.CreateLoginRequest(email, password));

        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        using var document = JsonDocument.Parse(await loginResponse.Content.ReadAsStringAsync());
        var token = document.RootElement.GetProperty("accessToken").GetString();

        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await Client.GetAsync("/api/offers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}