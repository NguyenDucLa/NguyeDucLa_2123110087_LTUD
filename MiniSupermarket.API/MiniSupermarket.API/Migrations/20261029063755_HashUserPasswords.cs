using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class HashUserPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED3wRxGMUeaNMgjRpd1Qwv4ZlslSctsUwxSpc5HRWJHdMnkzp8Z6K31HQpqU1zFuvg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                column: "Password",
                value: "123456");
        }
    }
}
