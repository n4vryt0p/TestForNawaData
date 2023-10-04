using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasterTransactionId = table.Column<int>(type: "int", nullable: true),
                    DebitCreditStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<long>(type: "bigint", nullable: true),
                    Point = table.Column<long>(type: "bigint", nullable: true, computedColumnSql: "CASE\r\n                        WHEN MasterTransactionId = 2 AND Amount >= 10001 AND Amount <= 30000 THEN FLOOR(Amount / 1000)\r\n                        WHEN MasterTransactionId = 2 AND Amount > 30000 THEN FLOOR(Amount / 1000) * 2\r\n                        WHEN MasterTransactionId = 3 AND Amount >= 50001 AND Amount <= 100000 THEN FLOOR(Amount / 2000)\r\n                        WHEN MasterTransactionId = 3 AND Amount > 100000 THEN FLOOR(Amount / 2000) * 2\r\n                        ELSE 0\r\n                    END")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_MasterTransactions_MasterTransactionId",
                        column: x => x.MasterTransactionId,
                        principalTable: "MasterTransactions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Customer 1" });

            migrationBuilder.InsertData(
                table: "MasterTransactions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Setor Tunai" },
                    { 2, "Beli Pulsa" },
                    { 3, "Bayar Listrik" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MasterTransactionId",
                table: "Transactions",
                column: "MasterTransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "MasterTransactions");
        }
    }
}
