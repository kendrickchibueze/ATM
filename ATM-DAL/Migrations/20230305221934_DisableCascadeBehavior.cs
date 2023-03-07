using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_DAL.Migrations
{
    /// <inheritdoc />
    public partial class DisableCascadeBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoFrom",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoFrom",
                table: "Transactions",
                column: "BankAccountNoFrom",
                principalTable: "BankAccounts",
                principalColumn: "CardNumber",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoFrom",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoFrom",
                table: "Transactions",
                column: "BankAccountNoFrom",
                principalTable: "BankAccounts",
                principalColumn: "CardNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
