using Microsoft.EntityFrameworkCore.Migrations;

namespace cChat.Data.Migrations
{
    public partial class MakingKeyinbottablenotnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bots_Key",
                table: "Bots");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Bots",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_Key",
                table: "Bots",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bots_Key",
                table: "Bots");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Bots",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_Key",
                table: "Bots",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");
        }
    }
}
