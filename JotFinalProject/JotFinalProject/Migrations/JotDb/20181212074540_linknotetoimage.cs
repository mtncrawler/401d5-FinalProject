using Microsoft.EntityFrameworkCore.Migrations;

namespace JotFinalProject.Migrations.JotDb
{
    public partial class linknotetoimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteID",
                table: "ImageUploadeds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageUploadeds_NoteID",
                table: "ImageUploadeds",
                column: "NoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageUploadeds_Notes_NoteID",
                table: "ImageUploadeds",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageUploadeds_Notes_NoteID",
                table: "ImageUploadeds");

            migrationBuilder.DropIndex(
                name: "IX_ImageUploadeds_NoteID",
                table: "ImageUploadeds");

            migrationBuilder.DropColumn(
                name: "NoteID",
                table: "ImageUploadeds");
        }
    }
}
