using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update_ChapterToChapterRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Chapter",
                table: "Books",
                newName: "ChapterTo");

            migrationBuilder.AddColumn<int>(
                name: "ChapterFrom",
                table: "Books",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChapterFrom",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ChapterTo",
                table: "Books",
                newName: "Chapter");
        }
    }
}
