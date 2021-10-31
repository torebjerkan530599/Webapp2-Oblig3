using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class subscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostsPostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagsTagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17934a71-4d00-475f-aa51-e2f3c41054fb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4147bd20-8c56-4a2e-afe9-010280971ce5");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

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
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");

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

            migrationBuilder.CreateTable(
                name: "BlogApplicationUser",
                columns: table => new
                {
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogApplicationUser", x => new { x.BlogId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_BlogApplicationUser_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogApplicationUser_Blogg_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogg",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsAdmin", "IsEnabled", "LastLoggedIn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "SurName", "Token", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6aea4ee8-2198-493c-b3ce-86dc27c98bbc", 10, null, "parafin@rock.com", true, null, null, null, null, false, null, "PARAFIN@ROCK.COM", "PELLE", null, "AQAAAAEAACcQAAAAEC4ycQ+ZpdWRuupCwxTff1lhblAh4tgaIL6Fq0eCkH6TF++ehjx4IsAsYhJ22BqT8g==", null, true, null, null, null, null, false, "Pelle" },
                    { "0e1b77fc-7587-444f-a08a-77bf82694472", 10, null, "harry@dirtymail.com", true, null, null, null, null, false, null, "HARRY@DIRTYMAIL.COM", "HARRY", null, "AQAAAAEAACcQAAAAEMlYpWcy/A9ALLPXyQ3xH7PpIKVTTGlaVtmkl2bIO1pbKTS1ZxKykZ6JopjnWftVzg==", null, true, null, null, null, null, false, "Harry" }
                });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 31, 20, 28, 26, 456, DateTimeKind.Local).AddTicks(6184));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 31, 20, 28, 26, 456, DateTimeKind.Local).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 31, 20, 28, 26, 456, DateTimeKind.Local).AddTicks(7682));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 31, 20, 28, 26, 456, DateTimeKind.Local).AddTicks(1836));

            migrationBuilder.UpdateData(
                table: "Post",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 31, 20, 28, 26, 456, DateTimeKind.Local).AddTicks(7633));

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 31, 20, 28, 26, 450, DateTimeKind.Local).AddTicks(8389), new DateTime(2021, 10, 31, 20, 28, 26, 455, DateTimeKind.Local).AddTicks(6666), "6aea4ee8-2198-493c-b3ce-86dc27c98bbc" });

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 2,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 31, 20, 28, 26, 455, DateTimeKind.Local).AddTicks(9157), new DateTime(2021, 10, 31, 20, 28, 26, 455, DateTimeKind.Local).AddTicks(9182), "0e1b77fc-7587-444f-a08a-77bf82694472" });

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 3,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 31, 20, 28, 26, 455, DateTimeKind.Local).AddTicks(9232), new DateTime(2021, 10, 31, 20, 28, 26, 455, DateTimeKind.Local).AddTicks(9239), "6aea4ee8-2198-493c-b3ce-86dc27c98bbc" });

            migrationBuilder.UpdateData(
                table: "Blogg",
                keyColumn: "BlogId",
                keyValue: 4,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 31, 20, 28, 26, 455, DateTimeKind.Local).AddTicks(9280), new DateTime(2021, 10, 31, 20, 28, 26, 455, DateTimeKind.Local).AddTicks(9288), "6aea4ee8-2198-493c-b3ce-86dc27c98bbc" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplicationUser_OwnerId",
                table: "BlogApplicationUser",
                column: "OwnerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Post_PostsPostId",
                table: "PostTag",
                column: "PostsPostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tag_TagsTagId",
                table: "PostTag",
                column: "TagsTagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Post_PostsPostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tag_TagsTagId",
                table: "PostTag");

            migrationBuilder.DropTable(
                name: "BlogApplicationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogg",
                table: "Blogg");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e1b77fc-7587-444f-a08a-77bf82694472");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6aea4ee8-2198-493c-b3ce-86dc27c98bbc");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

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
                name: "PK_Tags",
                table: "Tags",
                column: "TagId");

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsAdmin", "IsEnabled", "LastLoggedIn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "SurName", "Token", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4147bd20-8c56-4a2e-afe9-010280971ce5", 10, null, "parafin@rock.com", true, null, null, null, null, false, null, "PARAFIN@ROCK.COM", "PELLE", null, null, null, true, null, null, null, null, false, "Pelle" },
                    { "17934a71-4d00-475f-aa51-e2f3c41054fb", 10, null, "harry@dirtymail.com", true, null, null, null, null, false, null, "HARRY@DIRTYMAIL.COM", "HARRY", null, null, null, true, null, null, null, null, false, "Harry" }
                });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(9905));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 30, 17, 25, 50, 322, DateTimeKind.Local).AddTicks(1228));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 30, 17, 25, 50, 322, DateTimeKind.Local).AddTicks(1340));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(5724));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 30, 17, 25, 50, 322, DateTimeKind.Local).AddTicks(1295));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 30, 17, 25, 50, 316, DateTimeKind.Local).AddTicks(5030), new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(1342), "4147bd20-8c56-4a2e-afe9-010280971ce5" });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 2,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(3073), new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(3096), "17934a71-4d00-475f-aa51-e2f3c41054fb" });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 3,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(3141), new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(3149), "4147bd20-8c56-4a2e-afe9-010280971ce5" });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 4,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(3185), new DateTime(2021, 10, 30, 17, 25, 50, 321, DateTimeKind.Local).AddTicks(3192), "4147bd20-8c56-4a2e-afe9-010280971ce5" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostsPostId",
                table: "PostTag",
                column: "PostsPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagsTagId",
                table: "PostTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
