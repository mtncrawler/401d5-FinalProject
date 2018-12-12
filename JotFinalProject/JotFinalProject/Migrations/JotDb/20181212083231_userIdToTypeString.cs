using Microsoft.EntityFrameworkCore.Migrations;

namespace JotFinalProject.Migrations.JotDb
{
    public partial class userIdToTypeString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Notes",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Notes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
