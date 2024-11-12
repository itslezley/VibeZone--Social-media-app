using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaPlatform.Migrations
{
    /// <inheritdoc />
    public partial class SecondTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserDetailsID);
                });

            migrationBuilder.CreateTable(
                name: "Creators",
                columns: table => new
                {
                    CreatorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Followers = table.Column<int>(type: "int", nullable: false),
                    Following = table.Column<int>(type: "int", nullable: false),
                    NumberOfPost = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PredictionImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserDetailsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creators", x => x.CreatorID);
                    table.ForeignKey(
                        name: "FK_Creators_UserDetails_UserDetailsID",
                        column: x => x.UserDetailsID,
                        principalTable: "UserDetails",
                        principalColumn: "UserDetailsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creators_UserDetailsID",
                table: "Creators",
                column: "UserDetailsID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Creators");

            migrationBuilder.DropTable(
                name: "UserDetails");
        }
    }
}
