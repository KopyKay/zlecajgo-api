using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZlecajGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DefaultSqlValueChangeForOfferExpiryDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDateTime",
                table: "Offers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC' + INTERVAL '2 days'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDateTime",
                table: "Offers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC' + INTERVAL '2 days'");
        }
    }
}
