using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SplitBookIntoSeriesAndVolumes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookCollections_Books_BookId",
                table: "UserBookCollections");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropColumn(
                name: "IsOwned",
                table: "UserBookCollections");

            migrationBuilder.DropColumn(
                name: "IsWishlist",
                table: "UserBookCollections");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "UserBookCollections",
                newName: "BookSeriesId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBookCollections_BookId",
                table: "UserBookCollections",
                newName: "IX_UserBookCollections_BookSeriesId");

            migrationBuilder.CreateTable(
                name: "BookSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Illustrator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Translator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookSeries_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BookVolumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolumeNumber = table.Column<int>(type: "int", nullable: false),
                    ChapterFrom = table.Column<int>(type: "int", nullable: false),
                    ChapterTo = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookSeriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookVolumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookVolumes_BookSeries_BookSeriesId",
                        column: x => x.BookSeriesId,
                        principalTable: "BookSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookSeries_CreatedByUserId",
                table: "BookSeries",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookVolumes_BookSeriesId",
                table: "BookVolumes",
                column: "BookSeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookCollections_BookSeries_BookSeriesId",
                table: "UserBookCollections",
                column: "BookSeriesId",
                principalTable: "BookSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookCollections_BookSeries_BookSeriesId",
                table: "UserBookCollections");

            migrationBuilder.DropTable(
                name: "BookVolumes");

            migrationBuilder.DropTable(
                name: "BookSeries");

            migrationBuilder.RenameColumn(
                name: "BookSeriesId",
                table: "UserBookCollections",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBookCollections_BookSeriesId",
                table: "UserBookCollections",
                newName: "IX_UserBookCollections_BookId");

            migrationBuilder.AddColumn<bool>(
                name: "IsOwned",
                table: "UserBookCollections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWishlist",
                table: "UserBookCollections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChapterFrom = table.Column<int>(type: "int", nullable: true),
                    ChapterTo = table.Column<int>(type: "int", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Illustrator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Translator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedByUserId",
                table: "Books",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookCollections_Books_BookId",
                table: "UserBookCollections",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
