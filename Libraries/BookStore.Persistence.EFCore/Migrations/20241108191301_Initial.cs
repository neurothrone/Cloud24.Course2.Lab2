using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Persistence.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Isbn13 = table.Column<string>(type: "TEXT", maxLength: 17, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Language = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    Pages = table.Column<int>(type: "INTEGER", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Isbn13);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InventoryBalances",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "INTEGER", nullable: false),
                    Isbn13 = table.Column<string>(type: "TEXT", maxLength: 17, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBalances", x => new { x.StoreId, x.Isbn13 });
                    table.ForeignKey(
                        name: "FK_InventoryBalances_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryBalances_Books_Isbn13",
                        column: x => x.Isbn13,
                        principalTable: "Books",
                        principalColumn: "Isbn13",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryBalances_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Austen" },
                    { 2, new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "George", "Orwell" },
                    { 3, new DateTime(1835, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark", "Twain" },
                    { 4, new DateTime(1882, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Virginia", "Woolf" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Kungsgatan 34, 411 19 Göteborg", "Adlibris" },
                    { 2, "Nordstan, Norra Hamngatan 26, 411 06 Göteborg", "Akademibokhandeln" },
                    { 3, "Götabergsgatan 17 411 34 Göteborg", "Campusbokhandeln" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Isbn13", "AuthorId", "Language", "Pages", "Price", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { "9780099511408", 4, "English", 194, 12.99m, new DateTime(1925, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mrs. Dalloway" },
                    { "9780141439471", 1, "English", 392, 9.99m, new DateTime(1811, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sense and Sensibility" },
                    { "9780141439518", 1, "English", 279, 9.99m, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pride and Prejudice" },
                    { "9780451524935", 2, "English", 328, 14.99m, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1984" },
                    { "9781451628963", 3, "English", 274, 7.99m, new DateTime(1876, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Adventures of Tom Sawyer" }
                });

            migrationBuilder.InsertData(
                table: "InventoryBalances",
                columns: new[] { "Isbn13", "StoreId", "AuthorId", "Quantity" },
                values: new object[,]
                {
                    { "9780141439471", 1, null, 3 },
                    { "9780141439518", 1, null, 10 },
                    { "9780141439518", 2, null, 5 },
                    { "9780451524935", 2, null, 15 },
                    { "9781451628963", 2, null, 7 },
                    { "9780099511408", 3, null, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBalances_AuthorId",
                table: "InventoryBalances",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBalances_Isbn13",
                table: "InventoryBalances",
                column: "Isbn13");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryBalances");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
