using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FastFood.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Subtotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    IsFinished = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_cart", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_order_status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order_status", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_payment_status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PaymentStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_payment_status", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_product_tb_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    PayemntId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_order_tb_cart_CartId",
                        column: x => x.CartId,
                        principalTable: "tb_cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_order_tb_order_status_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "tb_order_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_order_tb_user_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_cart_item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_cart_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_cart_item_tb_cart_CartId",
                        column: x => x.CartId,
                        principalTable: "tb_cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_cart_item_tb_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tb_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PaymentIdMP = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Method = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_payment_tb_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tb_order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_payment_tb_payment_status_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "tb_payment_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "tb_cart",
                columns: new[] { "Id", "IsFinished", "Subtotal", "UserId" },
                values: new object[] { 1, false, 25.00m, new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "tb_category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Lanche" },
                    { 2, "Acompanhamento" },
                    { 3, "Bebida" },
                    { 4, "Sobremesa" }
                });

            migrationBuilder.InsertData(
                table: "tb_order_status",
                columns: new[] { "Id", "OrderStatus" },
                values: new object[,]
                {
                    { 1, "Received" },
                    { 2, "InPreparation" },
                    { 3, "Ready" },
                    { 4, "Finished" }
                });

            migrationBuilder.InsertData(
                table: "tb_payment_status",
                columns: new[] { "Id", "PaymentStatus" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "InProcess" },
                    { 3, "Approved" },
                    { 4, "Cancelled" },
                    { 5, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "tb_user",
                columns: new[] { "Id", "Email", "Name", "Password", "UserRole", "TaxId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "customer@email.com", "customer", "customer", 1, "88888888888" },
                    { new Guid("27cd36a3-816a-4a0e-9dab-5893908769fa"), "admin@email.com", "admin", "admin", 0, "99999999999" }
                });

            migrationBuilder.InsertData(
                table: "tb_product",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "Pão, carne, queijo, alface e tomate", "X-Burguer", 15.00m, 100 },
                    { 2, 2, "Batata frita crocante", "Batata Frita", 10.00m, 50 }
                });

            migrationBuilder.InsertData(
                table: "tb_cart_item",
                columns: new[] { "Id", "CartId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_cart_item_CartId",
                table: "tb_cart_item",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_cart_item_ProductId",
                table: "tb_cart_item",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_order_CartId",
                table: "tb_order",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_order_OrderStatusId",
                table: "tb_order",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_order_UserId",
                table: "tb_order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_payment_OrderId",
                table: "tb_payment",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_payment_PaymentStatusId",
                table: "tb_payment",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_CategoryId",
                table: "tb_product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_cart_item");

            migrationBuilder.DropTable(
                name: "tb_payment");

            migrationBuilder.DropTable(
                name: "tb_product");

            migrationBuilder.DropTable(
                name: "tb_order");

            migrationBuilder.DropTable(
                name: "tb_payment_status");

            migrationBuilder.DropTable(
                name: "tb_category");

            migrationBuilder.DropTable(
                name: "tb_cart");

            migrationBuilder.DropTable(
                name: "tb_order_status");

            migrationBuilder.DropTable(
                name: "tb_user");
        }
    }
}
