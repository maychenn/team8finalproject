using Microsoft.EntityFrameworkCore.Migrations;

namespace team8finalproject.Migrations
{
    public partial class Models1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Payees_PayeeID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PayeeID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PayeeID",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "PortfolioDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockID",
                table: "PortfolioDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioDetails_ProductID",
                table: "PortfolioDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioDetails_StockID",
                table: "PortfolioDetails",
                column: "StockID");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioDetails_Products_ProductID",
                table: "PortfolioDetails",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioDetails_Stocks_StockID",
                table: "PortfolioDetails",
                column: "StockID",
                principalTable: "Stocks",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioDetails_Products_ProductID",
                table: "PortfolioDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioDetails_Stocks_StockID",
                table: "PortfolioDetails");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioDetails_ProductID",
                table: "PortfolioDetails");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioDetails_StockID",
                table: "PortfolioDetails");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "PortfolioDetails");

            migrationBuilder.DropColumn(
                name: "StockID",
                table: "PortfolioDetails");

            migrationBuilder.AddColumn<int>(
                name: "PayeeID",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PayeeID",
                table: "Transactions",
                column: "PayeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Payees_PayeeID",
                table: "Transactions",
                column: "PayeeID",
                principalTable: "Payees",
                principalColumn: "PayeeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
