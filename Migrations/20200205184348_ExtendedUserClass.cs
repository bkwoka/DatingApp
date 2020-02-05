using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Migrations
{
    public partial class ExtendedUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UsersController",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UsersController",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UsersController",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "UsersController",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UsersController",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "UsersController",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "UsersController",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KnownAs",
                table: "UsersController",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                table: "UsersController",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LookingFor",
                table: "UsersController",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersController",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropColumn(
                name: "City",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "Interests",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "KnownAs",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "LastActive",
                table: "UsersController");

            migrationBuilder.DropColumn(
                name: "LookingFor",
                table: "UsersController");
        }
    }
}
