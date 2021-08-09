using Microsoft.EntityFrameworkCore.Migrations;

namespace cChat.Data.Migrations
{
    public partial class AddingKeytoBottable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Bots",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_Name",
                table: "ChatRooms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_Key",
                table: "Bots",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_Name",
                table: "Bots",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_Name",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_Bots_Key",
                table: "Bots");

            migrationBuilder.DropIndex(
                name: "IX_Bots_Name",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Bots");
        }
    }
}
