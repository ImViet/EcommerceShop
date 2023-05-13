using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceShop.Data.Migrations
{
    public partial class ChangeFileSizeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2fbad0cb-81ec-40ea-a2b1-35b1e311084c", "AQAAAAIAAYagAAAAEKfBRwioa5rUThZjRpwuqQmO3QbtbZrZV21Gl7FOitK5mq+bVztXyij+rhDm6SGbgA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 5, 13, 10, 49, 12, 113, DateTimeKind.Local).AddTicks(4011));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f6625b39-bee0-4b8c-9c5f-5dcc6b6487ee", "AQAAAAIAAYagAAAAEOZlGxbFImfXXhWMdqkOOwekGTlwK8WLhjpQwPAe7A8msuhCEuKDMDZfs1T5eUlymQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 5, 13, 0, 8, 9, 831, DateTimeKind.Local).AddTicks(7748));
        }
    }
}
