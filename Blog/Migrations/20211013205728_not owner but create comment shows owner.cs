using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Blog.Migrations
{
    public partial class notownerbutcreatecommentshowsowner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClosedForPosts",
                table: "BloggViewModel",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "BloggViewModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "BloggViewModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "BloggViewModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 426, DateTimeKind.Local).AddTicks(6794));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 433, DateTimeKind.Local).AddTicks(1418));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 433, DateTimeKind.Local).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 433, DateTimeKind.Local).AddTicks(1780));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 433, DateTimeKind.Local).AddTicks(7678));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 433, DateTimeKind.Local).AddTicks(8972));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 433, DateTimeKind.Local).AddTicks(9090));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 433, DateTimeKind.Local).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 13, 22, 57, 27, 433, DateTimeKind.Local).AddTicks(9043));

            migrationBuilder.CreateIndex(
                name: "IX_BloggViewModel_OwnerId",
                table: "BloggViewModel",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloggViewModel_AspNetUsers_OwnerId",
                table: "BloggViewModel",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloggViewModel_AspNetUsers_OwnerId",
                table: "BloggViewModel");

            migrationBuilder.DropIndex(
                name: "IX_BloggViewModel_OwnerId",
                table: "BloggViewModel");

            migrationBuilder.DropColumn(
                name: "ClosedForPosts",
                table: "BloggViewModel");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "BloggViewModel");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "BloggViewModel");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BloggViewModel");

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 797, DateTimeKind.Local).AddTicks(3414));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 804, DateTimeKind.Local).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 804, DateTimeKind.Local).AddTicks(2123));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 804, DateTimeKind.Local).AddTicks(2176));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 804, DateTimeKind.Local).AddTicks(8380));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 804, DateTimeKind.Local).AddTicks(9747));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 804, DateTimeKind.Local).AddTicks(9862));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 804, DateTimeKind.Local).AddTicks(4594));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 20, 14, 13, 804, DateTimeKind.Local).AddTicks(9817));
        }
    }
}
