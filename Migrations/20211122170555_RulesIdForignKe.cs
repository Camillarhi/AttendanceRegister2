using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceRegister2.Migrations
{
    public partial class RulesIdForignKe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rules_RulesId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RulesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RulesId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RulesId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RulesId",
                table: "AspNetUsers",
                column: "RulesId");

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
