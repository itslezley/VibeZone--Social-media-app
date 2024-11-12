using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCreatorRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    PhotoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.PhotoID);
                    table.ForeignKey(
                        name: "FK_Photo_Creators_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Creators",
                        principalColumn: "CreatorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Reactions = table.Column<int>(type: "int", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Post_Creators_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Creators",
                        principalColumn: "CreatorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    VideoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.VideoID);
                    table.ForeignKey(
                        name: "FK_Video_Creators_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Creators",
                        principalColumn: "CreatorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_CreatorID",
                table: "Photo",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CreatorID",
                table: "Post",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Video_CreatorID",
                table: "Video",
                column: "CreatorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Video");
        }
    }
}
