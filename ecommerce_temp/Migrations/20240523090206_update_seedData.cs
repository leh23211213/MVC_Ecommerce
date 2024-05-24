using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ecommerce_temp.Migrations
{
    /// <inheritdoc />
    public partial class update_seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "2");

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartItemId",
                keyValue: "1",
                column: "ProductId",
                value: "IP13MiniBK128GB");

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartItemId",
                keyValue: "2",
                column: "ProductId",
                value: "IP13MiniPK128GB");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: "1",
                column: "UserId",
                value: "1508df73-fedc-4196-a720-68ddf118bb6b");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity", "Vote" },
                values: new object[,]
                {
                    { "IP13MiniBK128GB", null, null, "~/lib/image/SmartPhone/Iphone/IP13-Mini-BK-128GB.png", "iPhone 13 Mini", 999m, null, null },
                    { "IP13MiniPK128GB", null, null, "~/lib/image/SmartPhone/Iphone/IP13-Mini-PK-128GB.png", "iPhone 13 Mini", 999m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "IP13MiniBK128GB");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "IP13MiniPK128GB");

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartItemId",
                keyValue: "1",
                column: "ProductId",
                value: "1");

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartItemId",
                keyValue: "2",
                column: "ProductId",
                value: "2");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: "1",
                column: "UserId",
                value: "user1");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity", "Vote" },
                values: new object[,]
                {
                    { "1", null, null, "/images/product1.png", "Product 1", 100m, null, null },
                    { "2", null, null, "/images/product2.png", "Product 2", 200m, null, null }
                });
        }
    }
}
