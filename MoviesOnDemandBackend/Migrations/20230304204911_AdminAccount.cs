using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesOnDemandBackend.Migrations
{
    /// <inheritdoc />
    public partial class AdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountCreated", "Email", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { 1, new DateTime(2023, 3, 4, 0, 0, 0, 0, DateTimeKind.Local), "admin@mod.com", new byte[] { 101, 207, 129, 49, 5, 83, 83, 19, 163, 102, 249, 234, 3, 229, 125, 125, 12, 47, 212, 209, 188, 3, 205, 99, 136, 211, 218, 15, 125, 162, 83, 94, 225, 240, 223, 199, 85, 156, 249, 225, 87, 42, 207, 248, 147, 62, 247, 4, 240, 250, 0, 33, 56, 5, 243, 194, 82, 224, 199, 173, 187, 207, 161, 106 }, new byte[] { 205, 52, 30, 120, 71, 17, 147, 136, 125, 20, 15, 141, 152, 58, 207, 205, 172, 21, 187, 229, 69, 205, 134, 212, 50, 31, 144, 175, 251, 163, 82, 153, 28, 207, 203, 126, 71, 29, 54, 66, 134, 32, 62, 39, 199, 94, 186, 95, 177, 14, 220, 92, 82, 238, 114, 2, 70, 208, 1, 156, 63, 60, 89, 93, 87, 189, 233, 246, 192, 146, 226, 186, 41, 219, 124, 114, 25, 146, 16, 13, 164, 19, 11, 5, 119, 229, 173, 207, 66, 35, 17, 131, 38, 28, 12, 45, 187, 29, 150, 193, 213, 117, 204, 54, 127, 186, 63, 226, 238, 206, 143, 78, 118, 78, 165, 224, 158, 89, 208, 86, 125, 23, 89, 31, 112, 195, 105, 210 }, "admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
