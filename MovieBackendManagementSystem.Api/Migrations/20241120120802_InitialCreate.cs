using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieBackendManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmIntroduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmDirector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmPerformer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VideoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.FilmId);
                });

            migrationBuilder.CreateTable(
                name: "NewsInfos",
                columns: table => new
                {
                    NewInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MainImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsInfos", x => x.NewInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "NewsInfos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
