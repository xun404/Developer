using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Developer.Models;

namespace Developer.Data.Migrations
{
    public partial class migrateApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_AspNetUsers_OwnerId",
                table: "Apps");

            migrationBuilder.DropIndex(
                name: "IX_Apps_OwnerId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Apps");

            migrationBuilder.AddColumn<int>(
                name: "AppCategory",
                table: "Apps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppCreateTime",
                table: "Apps",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AppPlatform",
                table: "Apps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreaterId",
                table: "Apps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apps_CreaterId",
                table: "Apps",
                column: "CreaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_AspNetUsers_CreaterId",
                table: "Apps",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_AspNetUsers_CreaterId",
                table: "Apps");

            migrationBuilder.DropIndex(
                name: "IX_Apps_CreaterId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "AppCategory",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "AppCreateTime",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "AppPlatform",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "Apps");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Apps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apps_OwnerId",
                table: "Apps",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_AspNetUsers_OwnerId",
                table: "Apps",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
