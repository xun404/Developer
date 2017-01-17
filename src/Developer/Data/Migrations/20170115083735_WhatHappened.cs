using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Developer.Data.Migrations
{
    public partial class WhatHappened : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
