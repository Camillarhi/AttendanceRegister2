using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceRegister2.Migrations
{
    public partial class staffForr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Rules_RulesModelId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_RulesModelId",
                table: "Department");

            migrationBuilder.AlterColumn<int>(
                name: "RulesModelId",
                table: "Department",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RulesModelId",
                table: "Department",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
