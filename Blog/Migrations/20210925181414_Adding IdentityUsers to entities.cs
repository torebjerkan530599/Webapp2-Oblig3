using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class AddingApplicationUserstoentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Post",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Post_OwnerId",
                table: "Post",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_OwnerId",
                table: "Post",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_OwnerId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_OwnerId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Comment");

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 733, DateTimeKind.Local).AddTicks(9989));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 740, DateTimeKind.Local).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 741, DateTimeKind.Local).AddTicks(1));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 741, DateTimeKind.Local).AddTicks(55));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 741, DateTimeKind.Local).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 741, DateTimeKind.Local).AddTicks(7245));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 741, DateTimeKind.Local).AddTicks(7478));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 741, DateTimeKind.Local).AddTicks(2423));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 45, 10, 741, DateTimeKind.Local).AddTicks(7317));
        }
    }
}
