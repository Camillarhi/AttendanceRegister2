using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceRegister2.Migrations
{
    public partial class roleForeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndOfDay",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "StartOfDay",
                table: "Rules");

            migrationBuilder.AlterColumn<int>(
                name: "StartOfDayGracePeriod",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EndOfDayGracePeriod",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndOfDayHour",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndOfDayMinutes",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartOfDayHour",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartOfDayMinutes",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RulesModelId",
                table: "Department",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Department_RulesModelId",
                table: "Department",
                column: "RulesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Rules_RulesModelId",
                table: "Department",
                column: "RulesModelId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Rules_RulesModelId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_RulesModelId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "EndOfDayHour",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "EndOfDayMinutes",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "StartOfDayHour",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "StartOfDayMinutes",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "RulesModelId",
                table: "Department");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOfDayGracePeriod",
                table: "Rules",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOfDayGracePeriod",
                table: "Rules",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfDay",
                table: "Rules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartOfDay",
                table: "Rules",
                type: "datetime2",
                nullable: true);
        }
    }
}
