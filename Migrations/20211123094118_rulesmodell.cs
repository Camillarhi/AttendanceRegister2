using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceRegister2.Migrations
{
    public partial class rulesmodell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RulesModelId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RulesModelId",
                table: "AspNetUsers",
                column: "RulesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rules_RulesModelId",
                table: "AspNetUsers",
                column: "RulesModelId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rules_RulesModelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RulesModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RulesModelId",
                table: "AspNetUsers");
        }
    }
}
