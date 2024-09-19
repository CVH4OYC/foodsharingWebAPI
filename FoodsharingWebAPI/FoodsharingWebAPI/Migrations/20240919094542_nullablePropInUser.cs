using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodsharingWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class nullablePropInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Announcements",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreation",
                value: new DateTime(2024, 9, 19, 9, 45, 41, 817, DateTimeKind.Utc).AddTicks(8353));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 19, 9, 45, 41, 817, DateTimeKind.Utc).AddTicks(8600));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2024, 9, 19, 9, 45, 41, 817, DateTimeKind.Utc).AddTicks(8492));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Announcements",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreation",
                value: new DateTime(2024, 9, 18, 12, 21, 47, 137, DateTimeKind.Utc).AddTicks(61));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 18, 12, 21, 47, 137, DateTimeKind.Utc).AddTicks(350));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2024, 9, 18, 12, 21, 47, 137, DateTimeKind.Utc).AddTicks(214));
        }
    }
}
