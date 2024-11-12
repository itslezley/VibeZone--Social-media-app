using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddedContentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Creators_CreatorID",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Creators_CreatorID",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Creators_CreatorID",
                table: "Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Video",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_CreatorID",
                table: "Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_CreatorID",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Video",
                newName: "Videos");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_Post_CreatorID",
                table: "Posts",
                newName: "IX_Posts_CreatorID");

            migrationBuilder.AddColumn<int>(
                name: "PostID",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhotoID",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoID",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostID",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "VideoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "PostID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PhotoID",
                table: "Posts",
                column: "PhotoID",
                unique: true,
                filter: "[PhotoID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_VideoID",
                table: "Posts",
                column: "VideoID",
                unique: true,
                filter: "[VideoID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Creators_CreatorID",
                table: "Posts",
                column: "CreatorID",
                principalTable: "Creators",
                principalColumn: "CreatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Photos_PhotoID",
                table: "Posts",
                column: "PhotoID",
                principalTable: "Photos",
                principalColumn: "PhotoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Videos_VideoID",
                table: "Posts",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Creators_CreatorID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Photos_PhotoID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Videos_VideoID",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PhotoID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_VideoID",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PostID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "PhotoID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VideoID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostID",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Videos",
                newName: "Video");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CreatorID",
                table: "Post",
                newName: "IX_Post_CreatorID");

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "Video",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "Photo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Video",
                table: "Video",
                column: "VideoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "PostID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_Video_CreatorID",
                table: "Video",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_CreatorID",
                table: "Photo",
                column: "CreatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Creators_CreatorID",
                table: "Photo",
                column: "CreatorID",
                principalTable: "Creators",
                principalColumn: "CreatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Creators_CreatorID",
                table: "Post",
                column: "CreatorID",
                principalTable: "Creators",
                principalColumn: "CreatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Creators_CreatorID",
                table: "Video",
                column: "CreatorID",
                principalTable: "Creators",
                principalColumn: "CreatorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
