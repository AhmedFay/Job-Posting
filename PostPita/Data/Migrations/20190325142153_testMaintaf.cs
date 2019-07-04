using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PostPita.Migrations
{
    public partial class testMaintaf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeHours",
                table: "PostsModified");

            migrationBuilder.DropColumn(
                name: "PostState",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TimeHours",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "HourType",
                table: "PostsModified",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HourType",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostStatus",
                table: "Posts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourType",
                table: "PostsModified");

            migrationBuilder.DropColumn(
                name: "HourType",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostStatus",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "TimeHours",
                table: "PostsModified",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostState",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TimeHours",
                table: "Posts",
                nullable: true);
        }
    }
}
