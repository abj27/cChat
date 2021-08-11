using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cChat.Data.Migrations
{
    public partial class RemovingBotsAddingdefaultuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Bots_BotId",
                table: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Bots");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_BotId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "BotId",
                table: "ChatMessages");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00000000-0000-0000-0000-000000000000", 0, "fe69af41-a505-41b2-8b04-53157167cebc", null, false, false, null, null, "System", null, null, false, "a580c99e-9bb5-48bf-bf33-77a36d37ca7e", false, "System" });

            migrationBuilder.UpdateData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 8, 10, 19, 2, 4, 422, DateTimeKind.Unspecified).AddTicks(5339), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 8, 10, 19, 2, 4, 420, DateTimeKind.Unspecified).AddTicks(4658), new TimeSpan(0, -4, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000");

            migrationBuilder.AddColumn<int>(
                name: "BotId",
                table: "ChatMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "CreatedDate", "Key", "ModifiedDate", "Name" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 196, DateTimeKind.Unspecified).AddTicks(1788), new TimeSpan(0, -4, 0, 0, 0)), "", new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 196, DateTimeKind.Unspecified).AddTicks(1428), new TimeSpan(0, -4, 0, 0, 0)), "DefaultBot" });

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "CreatedDate", "Key", "ModifiedDate", "Name" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 196, DateTimeKind.Unspecified).AddTicks(2378), new TimeSpan(0, -4, 0, 0, 0)), "stock", new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 196, DateTimeKind.Unspecified).AddTicks(2371), new TimeSpan(0, -4, 0, 0, 0)), "StockQuotesBot" });

            migrationBuilder.UpdateData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 195, DateTimeKind.Unspecified).AddTicks(991), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 8, 9, 20, 54, 56, 192, DateTimeKind.Unspecified).AddTicks(9148), new TimeSpan(0, -4, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_BotId",
                table: "ChatMessages",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_Key",
                table: "Bots",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_Name",
                table: "Bots",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Bots_BotId",
                table: "ChatMessages",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
