using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceRegister2.Migrations
{
    public partial class newer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartOfDayHour = table.Column<int>(type: "int", nullable: false),
                    StartOfDayMinutes = table.Column<int>(type: "int", nullable: false),
                    StartOfDayGracePeriod = table.Column<int>(type: "int", nullable: false),
                    EndOfDayHour = table.Column<int>(type: "int", nullable: false),
                    EndOfDayMinutes = table.Column<int>(type: "int", nullable: false),
                    EndOfDayGracePeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");
        }
    }
}
