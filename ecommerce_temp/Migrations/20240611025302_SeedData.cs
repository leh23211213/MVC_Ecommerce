using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ecommerce_temp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "BrandId", "BrandName", "ImageUrl" },
                values: new object[] { 1, "Apple", "~/lib/image/Brands/Apple.png" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 1, "Smartphone" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "ColorId", "ColorName" },
                values: new object[,]
                {
                    { 1, "Black" },
                    { 2, "Pink" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "SizeId", "SizeName" },
                values: new object[,]
                {
                    { 1, "128GB" },
                    { 2, "256GB" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BrandId", "CategoryId", "Description", "ImageUrl", "Price", "ProductName", "Quantity", "Vote" },
                values: new object[,]
                {
                    { "IP13MiniBK128GB", 1, 1, null, "~/lib/image/SmartPhone/Iphone/IP13-Mini-BK-128GB.png", 999m, "iPhone 13 Mini", null, null },
                    { "IP13MiniPK128GB", 1, 1, null, "~/lib/image/SmartPhone/Iphone/IP13-Mini-PK-128GB.png", 999m, "iPhone 13 Mini", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "ColorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "ColorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "IP13MiniBK128GB");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "IP13MiniPK128GB");

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "SizeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1);
        }
    }
}
