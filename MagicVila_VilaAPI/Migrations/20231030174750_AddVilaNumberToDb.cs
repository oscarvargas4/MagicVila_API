using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilaVilaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVilaNumberToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VilaNumbers",
                columns: table => new
                {
                    VilaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VilaNumbers", x => x.VilaNo);
                });

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 30, 18, 47, 50, 758, DateTimeKind.Local).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 30, 18, 47, 50, 758, DateTimeKind.Local).AddTicks(6237));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 30, 18, 47, 50, 758, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 30, 18, 47, 50, 758, DateTimeKind.Local).AddTicks(6243));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 30, 18, 47, 50, 758, DateTimeKind.Local).AddTicks(6246));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VilaNumbers");

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 18, 11, 14, 6, 52, DateTimeKind.Local).AddTicks(5658));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 18, 11, 14, 6, 52, DateTimeKind.Local).AddTicks(5710));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 18, 11, 14, 6, 52, DateTimeKind.Local).AddTicks(5713));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 18, 11, 14, 6, 52, DateTimeKind.Local).AddTicks(5716));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 18, 11, 14, 6, 52, DateTimeKind.Local).AddTicks(5755));
        }
    }
}
