using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAircraft.Migrations
{
    public partial class UpdateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductDetailViewModelOrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductDetailViewModelOrderId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductDetailViewModel",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgreementStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetailViewModel", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_ProductDetailViewModel_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductDetailViewModelOrderId",
                table: "Products",
                column: "ProductDetailViewModelOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductDetailViewModelOrderId1",
                table: "Products",
                column: "ProductDetailViewModelOrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetailViewModel_ProductId",
                table: "ProductDetailViewModel",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDetailViewModel_ProductDetailViewModelOrderId",
                table: "Products",
                column: "ProductDetailViewModelOrderId",
                principalTable: "ProductDetailViewModel",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDetailViewModel_ProductDetailViewModelOrderId1",
                table: "Products",
                column: "ProductDetailViewModelOrderId1",
                principalTable: "ProductDetailViewModel",
                principalColumn: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDetailViewModel_ProductDetailViewModelOrderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDetailViewModel_ProductDetailViewModelOrderId1",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductDetailViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductDetailViewModelOrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductDetailViewModelOrderId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDetailViewModelOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDetailViewModelOrderId1",
                table: "Products");
        }
    }
}
