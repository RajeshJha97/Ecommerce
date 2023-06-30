using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAndCategoryToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 27, 9, 58, 2, 95, DateTimeKind.Local).AddTicks(6677), "Test Category Description", "Test Category", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2023, 6, 27, 9, 58, 2, 95, DateTimeKind.Local).AddTicks(6678), "Dummy Category Description", "Dummy Category", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("048234ff-e015-4087-92d5-3ae2a00f7a08"), 2, new DateTime(2023, 6, 27, 9, 58, 2, 95, DateTimeKind.Local).AddTicks(6580), "Dummy6 Description", "Dummy6 Name", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("29fcd54e-965e-432d-a761-168ba6d80f20"), 2, new DateTime(2023, 6, 27, 9, 58, 2, 95, DateTimeKind.Local).AddTicks(6576), "Dummy1 Description", "Dummy1 Name", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7cffcebb-c9cd-4036-a3ab-66d0dcffebed"), 1, new DateTime(2023, 6, 27, 9, 58, 2, 95, DateTimeKind.Local).AddTicks(6546), "Test Description", "Test Name", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("92e75459-35ac-4078-aff7-18e728953718"), 2, new DateTime(2023, 6, 27, 9, 58, 2, 95, DateTimeKind.Local).AddTicks(6579), "Dummy5 Description", "Dummy5 Name", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c57cd240-b7c3-4126-b763-bda4f85b7c02"), 2, new DateTime(2023, 6, 27, 9, 58, 2, 95, DateTimeKind.Local).AddTicks(6574), "Dummy Description", "Dummy Name", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d54e85d2-6b44-4d3e-9aef-e239ac80ea40"), 2, new DateTime(2023, 6, 27, 9, 58, 2, 95, DateTimeKind.Local).AddTicks(6577), "Dummy4 Description", "Dummy4 Name", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
