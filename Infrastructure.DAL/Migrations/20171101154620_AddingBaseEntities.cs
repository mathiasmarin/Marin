using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.DAL.Migrations
{
    public partial class AddingBaseEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetCategory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyBudget",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Income = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyBudget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyBudget_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostValue = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyBudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cost_BudgetCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BudgetCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cost_MonthlyBudget_MonthlyBudgetId",
                        column: x => x.MonthlyBudgetId,
                        principalTable: "MonthlyBudget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategory_UserId",
                table: "BudgetCategory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_CategoryId",
                table: "Cost",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_MonthlyBudgetId",
                table: "Cost",
                column: "MonthlyBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyBudget_UserId",
                table: "MonthlyBudget",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cost");

            migrationBuilder.DropTable(
                name: "BudgetCategory");

            migrationBuilder.DropTable(
                name: "MonthlyBudget");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
