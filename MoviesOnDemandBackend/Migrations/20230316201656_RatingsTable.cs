using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesOnDemandBackend.Migrations
{
    /// <inheritdoc />
    public partial class RatingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRatings",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "SumOfRatings",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Local), new byte[] { 209, 234, 193, 226, 75, 52, 114, 68, 13, 30, 65, 218, 211, 244, 196, 129, 123, 19, 37, 210, 113, 106, 181, 22, 43, 92, 167, 247, 168, 29, 35, 218, 167, 101, 16, 226, 27, 179, 28, 67, 201, 252, 132, 155, 176, 171, 163, 53, 193, 108, 14, 198, 221, 251, 87, 72, 143, 109, 79, 253, 179, 221, 6, 62 }, new byte[] { 150, 159, 126, 49, 132, 90, 146, 123, 187, 15, 98, 114, 170, 45, 92, 106, 84, 180, 224, 141, 106, 168, 104, 109, 217, 66, 22, 162, 3, 177, 90, 124, 43, 115, 79, 152, 84, 22, 45, 179, 99, 249, 96, 126, 19, 119, 249, 90, 131, 195, 202, 31, 26, 149, 230, 182, 158, 190, 53, 34, 71, 92, 67, 48, 234, 38, 65, 128, 185, 28, 103, 17, 214, 182, 28, 9, 237, 174, 64, 104, 139, 70, 108, 72, 136, 152, 16, 221, 163, 213, 148, 195, 23, 5, 40, 155, 144, 248, 2, 253, 227, 139, 68, 155, 231, 117, 117, 198, 174, 127, 244, 51, 251, 153, 151, 151, 72, 189, 144, 213, 18, 109, 220, 241, 118, 84, 5, 19 } });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRatings",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Movies",
                type: "decimal(2,1)",
                precision: 2,
                scale: 1,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SumOfRatings",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10,
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 3, 4, 0, 0, 0, 0, DateTimeKind.Local), new byte[] { 101, 207, 129, 49, 5, 83, 83, 19, 163, 102, 249, 234, 3, 229, 125, 125, 12, 47, 212, 209, 188, 3, 205, 99, 136, 211, 218, 15, 125, 162, 83, 94, 225, 240, 223, 199, 85, 156, 249, 225, 87, 42, 207, 248, 147, 62, 247, 4, 240, 250, 0, 33, 56, 5, 243, 194, 82, 224, 199, 173, 187, 207, 161, 106 }, new byte[] { 205, 52, 30, 120, 71, 17, 147, 136, 125, 20, 15, 141, 152, 58, 207, 205, 172, 21, 187, 229, 69, 205, 134, 212, 50, 31, 144, 175, 251, 163, 82, 153, 28, 207, 203, 126, 71, 29, 54, 66, 134, 32, 62, 39, 199, 94, 186, 95, 177, 14, 220, 92, 82, 238, 114, 2, 70, 208, 1, 156, 63, 60, 89, 93, 87, 189, 233, 246, 192, 146, 226, 186, 41, 219, 124, 114, 25, 146, 16, 13, 164, 19, 11, 5, 119, 229, 173, 207, 66, 35, 17, 131, 38, 28, 12, 45, 187, 29, 150, 193, 213, 117, 204, 54, 127, 186, 63, 226, 238, 206, 143, 78, 118, 78, 165, 224, 158, 89, 208, 86, 125, 23, 89, 31, 112, 195, 105, 210 } });
        }
    }
}
