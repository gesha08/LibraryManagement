using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 101, "English novelist, essayist, journalist and critic.", new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "George", "Orwell" },
                    { 102, "English novelist known primarily for her six major novels.", new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Austen" },
                    { 103, "American author and screenwriter.", new DateTime(1920, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ray", "Bradbury" }
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "RegistrationDate" },
                values: new object[,]
                {
                    { 101, "ivan.petrov@example.com", "Ivan", "Petrov", "+359888123456", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102, "maria.ivanova@example.com", "Maria", "Ivanova", "+359888987654", new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 103, "georgi.dimitrov@example.com", "Georgi", "Dimitrov", "+359887111222", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "ISBN", "Pages", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 101, 101, "A dystopian social science fiction novel.", "9780451524935", 328, 1949, "1984" },
                    { 102, 102, "A romantic novel of manners.", "9781503290563", 432, 1813, "Pride and Prejudice" },
                    { 103, 103, "A dystopian novel about censorship.", "9781451673319", 249, 1953, "Fahrenheit 451" },
                    { 104, 101, "A satirical allegorical novella.", "9780451526342", 144, 1945, "Animal Farm" }
                });

            migrationBuilder.InsertData(
                table: "BookReaders",
                columns: new[] { "BookId", "ReaderId", "BorrowedDate", "ReturnedDate" },
                values: new object[,]
                {
                    { 101, 101, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 101, 103, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 102, 101, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 103, 102, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 104, 103, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookReaders",
                keyColumns: new[] { "BookId", "ReaderId" },
                keyValues: new object[] { 101, 101 });

            migrationBuilder.DeleteData(
                table: "BookReaders",
                keyColumns: new[] { "BookId", "ReaderId" },
                keyValues: new object[] { 101, 103 });

            migrationBuilder.DeleteData(
                table: "BookReaders",
                keyColumns: new[] { "BookId", "ReaderId" },
                keyValues: new object[] { 102, 101 });

            migrationBuilder.DeleteData(
                table: "BookReaders",
                keyColumns: new[] { "BookId", "ReaderId" },
                keyValues: new object[] { 103, 102 });

            migrationBuilder.DeleteData(
                table: "BookReaders",
                keyColumns: new[] { "BookId", "ReaderId" },
                keyValues: new object[] { 104, 103 });

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 103);
        }
    }
}
