using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class AddedManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogg_AspNetUsers_OwnerId",
                table: "Blogg");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_OwnerId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blogg_BlogId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogg",
                table: "Blogg");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "Blogg",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_Post_OwnerId",
                table: "Posts",
                newName: "IX_Posts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_BlogId",
                table: "Posts",
                newName: "IX_Posts_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_OwnerId",
                table: "Comments",
                newName: "IX_Comments_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogg_OwnerId",
                table: "Blogs",
                newName: "IX_Blogs_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "BlogId");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagLabel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostsPostId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsPostId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostsPostId",
                        column: x => x.PostsPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 108, DateTimeKind.Local).AddTicks(7529));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 115, DateTimeKind.Local).AddTicks(7519));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 115, DateTimeKind.Local).AddTicks(7775));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 115, DateTimeKind.Local).AddTicks(7829));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(4375));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(6018));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(6139));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(362));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(6091));

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "Created", "Modified", "TagLabel" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(7950), null, "Jesus" },
                    { 2, new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(8788), null, "Gud" },
                    { 3, new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(8951), null, "Allah" },
                    { 4, new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(9048), null, "Odin" },
                    { 5, new DateTime(2021, 10, 19, 18, 27, 19, 116, DateTimeKind.Local).AddTicks(9314), null, "Freya" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsTagId",
                table: "PostTag",
                column: "TagsTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_OwnerId",
                table: "Blogs",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_OwnerId",
                table: "Comments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_OwnerId",
                table: "Posts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_OwnerId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_OwnerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_OwnerId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blogg");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_OwnerId",
                table: "Post",
                newName: "IX_Post_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_BlogId",
                table: "Post",
                newName: "IX_Post_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "Comment",
                newName: "IX_Comment_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_OwnerId",
                table: "Comment",
                newName: "IX_Comment_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_OwnerId",
                table: "Blogg",
                newName: "IX_Blogg_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogg",
                table: "Blogg",
                column: "BlogId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Blogg_AspNetUsers_OwnerId",
                table: "Blogg",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_OwnerId",
                table: "Post",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blogg_BlogId",
                table: "Post",
                column: "BlogId",
                principalTable: "Blogg",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
