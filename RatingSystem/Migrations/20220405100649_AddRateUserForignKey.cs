using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatingSystem.Migrations
{
    public partial class AddRateUserForignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_User_Id",
                table: "Rates",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_User_Id",
                table: "Rates",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_User_Id",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_User_Id",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Rates");
        }
    }
}
