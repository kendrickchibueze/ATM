using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATM_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedNavigationPropertiesOrForeignKeysToTransactionModelWithFluentApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "BankAccountsId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BankAccountsId1",
                table: "Transactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts_CardNumber",
                table: "BankAccounts",
                column: "CardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountNoFrom",
                table: "Transactions",
                column: "BankAccountNoFrom");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountNoTo",
                table: "Transactions",
                column: "BankAccountNoTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoFrom",
                table: "Transactions",
                column: "BankAccountNoFrom",
                principalTable: "BankAccounts",
                principalColumn: "CardNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoTo",
                table: "Transactions",
                column: "BankAccountNoTo",
                principalTable: "BankAccounts",
                principalColumn: "CardNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoFrom",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountNoTo",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BankAccountNoFrom",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BankAccountNoTo",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts_CardNumber",
                table: "BankAccounts");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "Id");

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
    }
}
