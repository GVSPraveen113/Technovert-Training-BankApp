using Microsoft.EntityFrameworkCore.Migrations;

namespace Technovert.BankApp.Services.Migrations
{
    public partial class Latest_Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Banks_BankId1",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "BankId1",
                table: "Accounts",
                newName: "BankId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_BankId1",
                table: "Accounts",
                newName: "IX_Accounts_BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Banks_BankId",
                table: "Accounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Banks_BankId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "Accounts",
                newName: "BankId1");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_BankId",
                table: "Accounts",
                newName: "IX_Accounts_BankId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Banks_BankId1",
                table: "Accounts",
                column: "BankId1",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
