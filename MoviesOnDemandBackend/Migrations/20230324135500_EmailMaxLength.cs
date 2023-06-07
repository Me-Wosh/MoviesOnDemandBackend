using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesOnDemandBackend.Migrations
{
    /// <inheritdoc />
    public partial class EmailMaxLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 80, 255, 50, 244, 191, 215, 102, 9, 121, 186, 47, 39, 10, 158, 111, 129, 115, 147, 41, 66, 128, 65, 82, 177, 56, 37, 12, 102, 186, 181, 243, 20, 79, 32, 103, 137, 146, 172, 206, 57, 231, 55, 187, 8, 172, 169, 162, 166, 28, 68, 152, 55, 117, 179, 224, 244, 70, 97, 199, 198, 232, 40, 61, 193 }, new byte[] { 128, 48, 226, 194, 0, 53, 245, 12, 97, 111, 216, 240, 125, 12, 174, 244, 132, 112, 215, 85, 118, 186, 113, 133, 199, 82, 235, 60, 186, 155, 160, 208, 134, 73, 186, 195, 166, 42, 244, 218, 0, 155, 157, 13, 137, 78, 239, 97, 181, 44, 215, 193, 93, 210, 117, 232, 81, 176, 222, 157, 46, 157, 184, 74, 72, 231, 144, 209, 129, 28, 99, 103, 231, 156, 112, 255, 133, 6, 68, 105, 216, 26, 105, 151, 144, 146, 4, 34, 228, 235, 33, 15, 202, 159, 228, 121, 137, 149, 97, 34, 81, 123, 200, 224, 37, 135, 83, 206, 71, 101, 179, 163, 6, 84, 13, 241, 206, 104, 217, 241, 199, 200, 216, 22, 55, 164, 123, 60 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 215, 179, 34, 141, 178, 133, 131, 240, 88, 150, 140, 207, 251, 154, 168, 245, 56, 15, 212, 54, 26, 61, 175, 198, 186, 108, 61, 239, 114, 211, 194, 85, 158, 30, 228, 36, 221, 70, 13, 31, 37, 7, 196, 18, 74, 1, 178, 251, 161, 15, 163, 161, 174, 174, 164, 182, 60, 231, 62, 44, 55, 206, 118, 170 }, new byte[] { 227, 204, 188, 76, 38, 32, 47, 224, 223, 15, 45, 186, 63, 131, 226, 95, 222, 242, 77, 133, 216, 181, 241, 171, 182, 230, 105, 120, 156, 75, 198, 81, 109, 119, 230, 202, 81, 45, 201, 207, 190, 228, 100, 41, 225, 249, 227, 204, 55, 52, 211, 124, 71, 226, 49, 155, 213, 50, 168, 27, 149, 78, 104, 129, 67, 181, 153, 146, 146, 189, 165, 120, 179, 41, 68, 175, 202, 51, 252, 94, 116, 162, 144, 251, 77, 239, 95, 106, 41, 154, 52, 217, 78, 50, 118, 107, 122, 17, 79, 9, 224, 58, 10, 77, 169, 253, 27, 41, 23, 186, 159, 32, 37, 179, 97, 78, 177, 29, 130, 196, 129, 88, 15, 4, 200, 78, 127, 104 } });
        }
    }
}
