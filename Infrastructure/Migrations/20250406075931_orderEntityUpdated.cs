using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orderEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Basket_BasketId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Basket_BasketId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BasketId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Books_BasketId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BasketId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Basket_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BasketId",
                table: "Books",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Basket_BasketId",
                table: "Books",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Basket_BasketId",
                table: "Orders",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
