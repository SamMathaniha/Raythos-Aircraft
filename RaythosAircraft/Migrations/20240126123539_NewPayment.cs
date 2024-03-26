using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaythosAircraft.Migrations
{
    public partial class NewPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardCVC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardExpiry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ProductDetailViewModel",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId1 = table.Column<int>(type: "int", nullable: false),
                    OrdersOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgreementStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetailViewModel", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_ProductDetailViewModel_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetailViewModel_Orders_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Orders_ProductDetailViewModelOrderId",
                table: "Orders",
                column: "ProductDetailViewModelOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetailViewModel_OrderId1",
                table: "ProductDetailViewModel",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetailViewModel_OrdersOrderId",
                table: "ProductDetailViewModel",
                column: "OrdersOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetailViewModel_ProductId",
                table: "ProductDetailViewModel",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ProductDetailViewModel_ProductDetailViewModelOrderId",
                table: "Orders",
                column: "ProductDetailViewModelOrderId",
                principalTable: "ProductDetailViewModel",
                principalColumn: "OrderId");

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
    }
}
