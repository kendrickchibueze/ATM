using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_DAL.Migrations
{
    /// <inheritdoc />
    public partial class DisableCascadeBehaviorForTransacToBankRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoTo",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoTo",
                table: "Transactions",
                column: "BankAccountNoTo",
                principalTable: "BankAccounts",
                principalColumn: "CardNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoTo",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoTo",
                table: "Transactions",
                column: "BankAccountNoTo",
                principalTable: "BankAccounts",
                principalColumn: "CardNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
