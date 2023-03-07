using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_DAL.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyRelationBtwBankAccountsAndTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankAccountsId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankAccountsId1",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountsId",
                table: "Transactions",
                column: "BankAccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountsId1",
                table: "Transactions",
                column: "BankAccountsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountsId",
                table: "Transactions",
                column: "BankAccountsId",
                principalTable: "BankAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountsId1",
                table: "Transactions",
                column: "BankAccountsId1",
                principalTable: "BankAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountsId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountsId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BankAccountsId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BankAccountsId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BankAccountsId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BankAccountsId1",
                table: "Transactions");
        }
    }
}
