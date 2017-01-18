using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Developer.Data.Migrations
{
    public partial class MigrateUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "headimgurl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nickname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "headimgurl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "nickname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "sex",
                table: "AspNetUsers");
        }
    }
}
