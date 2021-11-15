using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceRegister2.Migrations
{
    public partial class RulesIdForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rules_RulesId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RulesId",
                table: "AspNetUsers",
                newName: "RulesModelId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RulesId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RulesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rules_RulesModelId",
                table: "AspNetUsers",
                column: "RulesModelId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rules_RulesModelId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RulesModelId",
                table: "AspNetUsers",
                newName: "RulesId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RulesModelId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RulesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rules_RulesId",
                table: "AspNetUsers",
                column: "RulesId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
