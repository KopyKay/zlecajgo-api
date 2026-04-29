using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;
using FluentAssertions;
using ZlecajGo.API.IntegrationTests.Fixtures;
using ZlecajGo.API.IntegrationTests.Helpers.TestData;
using ZlecajGo.Application.Offers.Dtos;

namespace ZlecajGo.API.IntegrationTests;

public class OffersIntegrationTests : ApiTestBase
{
    [Fact]
    public async Task CreateOffer_WithValidToken_ReturnsCreated()
    {
        // Arrange
        var (token, _) = await RegisterAndLoginWithProfileAsync();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var offerRequest = OfferRequestTestData.CreateValidOffer();

        // Act
        var response = await Client.PostAsJsonAsync("/api/offers/create", offerRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GetOffers_ReturnsJsonCollection()
    {
        // Arrange
        var (token, _) = await RegisterAndLoginWithProfileAsync();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        await Client.PostAsJsonAsync("/api/offers/create", OfferRequestTestData.CreateValidOffer());

        // Act
        var response = await Client.GetAsync("/api/offers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var payload = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(payload);
        document.RootElement.ValueKind.Should().Be(JsonValueKind.Array);
    }

    [Fact]
    public async Task UpdateOffer_NotOwner_ReturnsForbiddenOrNotFound()
    {
        // Arrange
        var (ownerToken, ownerEmail) = await RegisterAndLoginWithProfileAsync();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", ownerToken);

        var createResponse = await Client.PostAsJsonAsync("/api/offers/create", OfferRequestTestData.CreateValidOffer());
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var offersResponse = await Client.GetAsync("/api/offers");
        offersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var offers = await offersResponse.Content.ReadFromJsonAsync<List<OfferDto>>() ?? [];
        var offerId = offers.First().Id;

        var (otherToken, _) = await RegisterAndLoginWithProfileAsync(excludeEmail: ownerEmail);
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", otherToken);

        var updateRequest = OfferRequestTestData.CreateUpdateOfferRequest();

        // Act
        var updateResponse = await Client.PatchAsJsonAsync($"/api/offers/update?offerId={offerId}", updateRequest);

        // Assert
        updateResponse.StatusCode.Should().BeOneOf(HttpStatusCode.Forbidden, HttpStatusCode.NotFound);
    }

    private async Task<(string token, string email)> RegisterAndLoginWithProfileAsync(string? excludeEmail = null)
    {
        var email = $"user_{Guid.NewGuid():N}@test.local";
        if (!string.IsNullOrEmpty(excludeEmail) && email == excludeEmail)
        {
            email = $"user_{Guid.NewGuid():N}@test.local";
        }

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
        var token = document.RootElement.GetProperty("accessToken").GetString()!;

        return (token, email);
    }
}