using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesOnDemandBackend.Migrations
{
    /// <inheritdoc />
    public partial class YearNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Movies",
                type: "int",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 3, 24, 0, 0, 0, 0, DateTimeKind.Local), new byte[] { 215, 179, 34, 141, 178, 133, 131, 240, 88, 150, 140, 207, 251, 154, 168, 245, 56, 15, 212, 54, 26, 61, 175, 198, 186, 108, 61, 239, 114, 211, 194, 85, 158, 30, 228, 36, 221, 70, 13, 31, 37, 7, 196, 18, 74, 1, 178, 251, 161, 15, 163, 161, 174, 174, 164, 182, 60, 231, 62, 44, 55, 206, 118, 170 }, new byte[] { 227, 204, 188, 76, 38, 32, 47, 224, 223, 15, 45, 186, 63, 131, 226, 95, 222, 242, 77, 133, 216, 181, 241, 171, 182, 230, 105, 120, 156, 75, 198, 81, 109, 119, 230, 202, 81, 45, 201, 207, 190, 228, 100, 41, 225, 249, 227, 204, 55, 52, 211, 124, 71, 226, 49, 155, 213, 50, 168, 27, 149, 78, 104, 129, 67, 181, 153, 146, 146, 189, 165, 120, 179, 41, 68, 175, 202, 51, 252, 94, 116, 162, 144, 251, 77, 239, 95, 106, 41, 154, 52, 217, 78, 50, 118, 107, 122, 17, 79, 9, 224, 58, 10, 77, 169, 253, 27, 41, 23, 186, 159, 32, 37, 179, 97, 78, 177, 29, 130, 196, 129, 88, 15, 4, 200, 78, 127, 104 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Movies",
                type: "int",
                maxLength: 4,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Local), new byte[] { 209, 234, 193, 226, 75, 52, 114, 68, 13, 30, 65, 218, 211, 244, 196, 129, 123, 19, 37, 210, 113, 106, 181, 22, 43, 92, 167, 247, 168, 29, 35, 218, 167, 101, 16, 226, 27, 179, 28, 67, 201, 252, 132, 155, 176, 171, 163, 53, 193, 108, 14, 198, 221, 251, 87, 72, 143, 109, 79, 253, 179, 221, 6, 62 }, new byte[] { 150, 159, 126, 49, 132, 90, 146, 123, 187, 15, 98, 114, 170, 45, 92, 106, 84, 180, 224, 141, 106, 168, 104, 109, 217, 66, 22, 162, 3, 177, 90, 124, 43, 115, 79, 152, 84, 22, 45, 179, 99, 249, 96, 126, 19, 119, 249, 90, 131, 195, 202, 31, 26, 149, 230, 182, 158, 190, 53, 34, 71, 92, 67, 48, 234, 38, 65, 128, 185, 28, 103, 17, 214, 182, 28, 9, 237, 174, 64, 104, 139, 70, 108, 72, 136, 152, 16, 221, 163, 213, 148, 195, 23, 5, 40, 155, 144, 248, 2, 253, 227, 139, 68, 155, 231, 117, 117, 198, 174, 127, 244, 51, 251, 153, 151, 151, 72, 189, 144, 213, 18, 109, 220, 241, 118, 84, 5, 19 } });
        }
    }
}
