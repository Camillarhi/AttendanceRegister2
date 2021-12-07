using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceRegister2.Migrations
{
    public partial class staffFor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RulesModelId",
                table: "Department",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Rules_RulesModelId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_RulesModelId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "RulesModelId",
                table: "Department");
        }
    }
}
