using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceShop.Data.Migrations
{
    public partial class addFieldOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "acb785bb-18dc-4e34-adb0-93f2cfdf89f9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f875bd9e-0ff1-4aab-8d9e-d6ada6823719", "AQAAAAEAACcQAAAAEAkdoijXCveNSHRCxPMepW5NAKGeTb6GNekFvtZ537USoEw0ovzaAsq0t7p1g8Nocg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 5, 22, 47, 10, 412, DateTimeKind.Local).AddTicks(31));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "No",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5a738490-10da-43fa-90e4-e147f5331371");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "07c2091e-4ee3-48ca-a9c4-a3b85b4a5e0c", "AQAAAAEAACcQAAAAEJuQjGt2sqiq9zBFyXIxTyXikcSc4z/Qo+4XIAzi0wLsLnDksFN0/yOmUkVcavnJ4Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 6, 23, 23, 51, 44, 316, DateTimeKind.Local).AddTicks(2275));
        }
    }
}
