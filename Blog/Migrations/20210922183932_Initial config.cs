using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class Initialconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogg",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClosedForPosts = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogg", x => x.BlogId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfComments = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_Blogg_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogg",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Blogg",
                columns: new[] { "BlogId", "ClosedForPosts", "Created", "Modified", "Name" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2021, 9, 22, 20, 39, 32, 53, DateTimeKind.Local).AddTicks(8295), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor" },
                    { 2, false, new DateTime(2021, 9, 22, 20, 39, 32, 60, DateTimeKind.Local).AddTicks(359), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quisque convallis est" },
                    { 3, false, new DateTime(2021, 9, 22, 20, 39, 32, 60, DateTimeKind.Local).AddTicks(568), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Interdum et malesuada" },
                    { 4, false, new DateTime(2021, 9, 22, 20, 39, 32, 60, DateTimeKind.Local).AddTicks(615), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauris mi velit" }
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Created", "Modified", "NumberOfComments", "Title" },
                values: new object[] { 1, 1, "Etiam vulputate massa id ante malesuada elementum. Nulla tellus purus, hendrerit rhoncus justo quis, accumsan ultrices nisi. Integer tristique, ligula in convallis aliquam, massa ligula vehicula odio, in eleifend dolor eros ut nunc", new DateTime(2021, 9, 22, 20, 39, 32, 60, DateTimeKind.Local).AddTicks(2935), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "To be or not to be" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Created", "Modified", "NumberOfComments", "Title" },
                values: new object[] { 2, 2, "Praesent non massa a nisl euismod efficitur. Ut laoreet nisi vel eleifend laoreet. Curabitur vel orci semper, auctor erat vel, dapibus nunc. Integer eget tortor nunc. Fusce ac euismod nibh. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae", new DateTime(2021, 9, 22, 20, 39, 32, 60, DateTimeKind.Local).AddTicks(9659), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "To be or not to be" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "Created", "Modified", "PostId", "Text" },
                values: new object[] { 1, new DateTime(2021, 9, 22, 20, 39, 32, 60, DateTimeKind.Local).AddTicks(7642), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Is this latin?" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "Created", "Modified", "PostId", "Text" },
                values: new object[] { 2, new DateTime(2021, 9, 22, 20, 39, 32, 60, DateTimeKind.Local).AddTicks(9592), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Yes, of course it is" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "Created", "Modified", "PostId", "Text" },
                values: new object[] { 3, new DateTime(2021, 9, 22, 20, 39, 32, 60, DateTimeKind.Local).AddTicks(9704), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "I really like the blog, but Quisque?" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_BlogId",
                table: "Post",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Blogg");
        }
    }
}
