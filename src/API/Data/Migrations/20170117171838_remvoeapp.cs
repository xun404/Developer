using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class remvoeapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    AppId = table.Column<string>(nullable: false),
                    AppCategory = table.Column<int>(nullable: false),
                    AppCreateTime = table.Column<DateTime>(nullable: false),
                    AppName = table.Column<string>(nullable: true),
                    AppPlatform = table.Column<int>(nullable: false),
                    AppSecret = table.Column<string>(nullable: true),
                    CreaterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.AppId);
                    table.ForeignKey(
                        name: "FK_App_AspNetUsers_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_CreaterId",
                table: "App",
                column: "CreaterId");
        }
    }
}
