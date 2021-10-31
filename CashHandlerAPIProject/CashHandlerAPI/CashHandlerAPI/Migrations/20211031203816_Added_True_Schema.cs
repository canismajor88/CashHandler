using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashHandlerAPI.Migrations
{
    public partial class Added_True_Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashBalance",
                columns: table => new
                {
                    CashBalanceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashBalance", x => x.CashBalanceId);
                });

            migrationBuilder.CreateTable(
                name: "MoneyAmount",
                columns: table => new
                {
                    MoneyAmountID = table.Column<long>(type: "bigint", nullable: false),
                    DollarCoinAmount = table.Column<int>(type: "int", nullable: true),
                    HalfDollarAmount = table.Column<int>(type: "int", nullable: true),
                    QuartersAmount = table.Column<int>(type: "int", nullable: true),
                    DimesAmount = table.Column<int>(type: "int", nullable: true),
                    NicklesAmount = table.Column<int>(type: "int", nullable: true),
                    PenniesAmount = table.Column<int>(type: "int", nullable: true),
                    HundredsAmount = table.Column<int>(type: "int", nullable: true),
                    FiftiesAmount = table.Column<int>(type: "int", nullable: true),
                    TwentiesAmount = table.Column<int>(type: "int", nullable: true),
                    TensAmount = table.Column<int>(type: "int", nullable: true),
                    FivesAmount = table.Column<int>(type: "int", nullable: true),
                    OnesAmount = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyAmount", x => x.MoneyAmountID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastSignIn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CashBalanceID = table.Column<long>(type: "bigint", nullable: true),
                    MoneyAmountID = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CashBalance_CashBalanceID",
                        column: x => x.CashBalanceID,
                        principalTable: "CashBalance",
                        principalColumn: "CashBalanceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_MoneyAmount",
                        column: x => x.MoneyAmountID,
                        principalTable: "MoneyAmount",
                        principalColumn: "MoneyAmountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Denominations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Users",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CashBalanceID",
                table: "AspNetUsers",
                column: "CashBalanceID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MoneyAmountID",
                table: "AspNetUsers",
                column: "MoneyAmountID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CashBalance");

            migrationBuilder.DropTable(
                name: "MoneyAmount");
        }
    }
}
