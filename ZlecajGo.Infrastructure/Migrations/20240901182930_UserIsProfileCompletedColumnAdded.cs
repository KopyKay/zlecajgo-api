using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZlecajGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserIsProfileCompletedColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProfileCompleted",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProfileCompleted",
                table: "AspNetUsers");
        }
    }
}
