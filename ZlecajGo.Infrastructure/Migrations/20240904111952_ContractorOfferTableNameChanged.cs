using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZlecajGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContractorOfferTableNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorOffer");

            migrationBuilder.CreateTable(
                name: "OfferContractors",
                columns: table => new
                {
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractorId = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferContractors", x => new { x.ContractorId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_OfferContractors_AspNetUsers_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferContractors_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferContractors_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferContractors_OfferId",
                table: "OfferContractors",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferContractors_StatusId",
                table: "OfferContractors",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferContractors");

            migrationBuilder.CreateTable(
                name: "ContractorOffer",
                columns: table => new
                {
                    ContractorId = table.Column<string>(type: "text", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorOffer", x => new { x.ContractorId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_ContractorOffer_AspNetUsers_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorOffer_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorOffer_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorOffer_OfferId",
                table: "ContractorOffer",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorOffer_StatusId",
                table: "ContractorOffer",
                column: "StatusId");
        }
    }
}
