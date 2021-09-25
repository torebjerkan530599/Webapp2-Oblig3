using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Blogg",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Blogg_OwnerId",
                table: "Blogg",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogg_AspNetUsers_OwnerId",
                table: "Blogg",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogg_AspNetUsers_OwnerId",
                table: "Blogg");

            migrationBuilder.DropIndex(
                name: "IX_Blogg_OwnerId",
                table: "Blogg");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Blogg");

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 895, DateTimeKind.Local).AddTicks(5016));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 902, DateTimeKind.Local).AddTicks(6500));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 902, DateTimeKind.Local).AddTicks(6844));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 902, DateTimeKind.Local).AddTicks(6905));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 903, DateTimeKind.Local).AddTicks(2943));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 903, DateTimeKind.Local).AddTicks(4303));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 903, DateTimeKind.Local).AddTicks(4425));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 902, DateTimeKind.Local).AddTicks(9318));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 9, 25, 16, 21, 0, 903, DateTimeKind.Local).AddTicks(4376));
        }
    }
}
