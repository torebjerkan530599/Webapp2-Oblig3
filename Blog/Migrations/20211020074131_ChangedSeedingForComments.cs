using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Blog.Migrations
{
    public partial class ChangedSeedingForComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 382, DateTimeKind.Local).AddTicks(4470));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 388, DateTimeKind.Local).AddTicks(9228));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 388, DateTimeKind.Local).AddTicks(9459));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 388, DateTimeKind.Local).AddTicks(9520));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 389, DateTimeKind.Local).AddTicks(5800));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 389, DateTimeKind.Local).AddTicks(7171));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 389, DateTimeKind.Local).AddTicks(7300));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 389, DateTimeKind.Local).AddTicks(2176));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 20, 9, 41, 30, 389, DateTimeKind.Local).AddTicks(7248));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
