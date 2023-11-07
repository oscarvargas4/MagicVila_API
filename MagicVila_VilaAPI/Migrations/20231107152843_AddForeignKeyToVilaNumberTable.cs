using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilaVilaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVilaNumberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VilaID",
                table: "VilaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 7, 16, 28, 42, 926, DateTimeKind.Local).AddTicks(5189));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 7, 16, 28, 42, 926, DateTimeKind.Local).AddTicks(5262));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 7, 16, 28, 42, 926, DateTimeKind.Local).AddTicks(5264));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 7, 16, 28, 42, 926, DateTimeKind.Local).AddTicks(5267));

            migrationBuilder.UpdateData(
                table: "Vilas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 7, 16, 28, 42, 926, DateTimeKind.Local).AddTicks(5269));

            migrationBuilder.CreateIndex(
                name: "IX_VilaNumbers_VilaID",
                table: "VilaNumbers",
                column: "VilaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VilaNumbers_Vilas_VilaID",
                table: "VilaNumbers",
                column: "VilaID",
                principalTable: "Vilas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VilaNumbers_Vilas_VilaID",
                table: "VilaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VilaNumbers_VilaID",
                table: "VilaNumbers");

            migrationBuilder.DropColumn(
                name: "VilaID",
                table: "VilaNumbers");

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
    }
}
