using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cChat.Data.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "CreatedDate", "Key", "ModifiedDate", "Name" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 196, DateTimeKind.Unspecified).AddTicks(1788), new TimeSpan(0, -4, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 196, DateTimeKind.Unspecified).AddTicks(1428), new TimeSpan(0, -4, 0, 0, 0)), "DefaultBot" });

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "CreatedDate", "Key", "ModifiedDate", "Name" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 196, DateTimeKind.Unspecified).AddTicks(2378), new TimeSpan(0, -4, 0, 0, 0)), "stock", new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 196, DateTimeKind.Unspecified).AddTicks(2371), new TimeSpan(0, -4, 0, 0, 0)), "StockQuotesBot" });

            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 195, DateTimeKind.Unspecified).AddTicks(991), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 192, DateTimeKind.Unspecified).AddTicks(9148), new TimeSpan(0, -4, 0, 0, 0)), "Default Chat Room" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
