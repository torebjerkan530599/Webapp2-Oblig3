using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class addblogowners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17934a71-4d00-475f-aa51-e2f3c41054fb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4147bd20-8c56-4a2e-afe9-010280971ce5");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsAdmin", "IsEnabled", "LastLoggedIn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "SurName", "Token", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5", 0, "9c22a64e-ca57-4401-bcd1-4387c1563923", "admin@gmail.com", false, null, null, null, null, false, null, null, null, null, null, "1234567890", false, null, "517024bc-5244-4709-be33-25fdc4cfc282", null, null, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 28, 17, 36, 25, 856, DateTimeKind.Local).AddTicks(7042), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 2,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 28, 17, 36, 25, 859, DateTimeKind.Local).AddTicks(3399), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 3,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 28, 17, 36, 25, 859, DateTimeKind.Local).AddTicks(3452), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "BlogId",
                keyValue: 4,
                columns: new[] { "Created", "Modified", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 28, 17, 36, 25, 859, DateTimeKind.Local).AddTicks(3472), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 28, 17, 36, 25, 859, DateTimeKind.Local).AddTicks(6282));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 28, 17, 36, 25, 859, DateTimeKind.Local).AddTicks(6916));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2021, 10, 28, 17, 36, 25, 859, DateTimeKind.Local).AddTicks(6968));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 10, 28, 17, 36, 25, 859, DateTimeKind.Local).AddTicks(4607));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2021, 10, 28, 17, 36, 25, 859, DateTimeKind.Local).AddTicks(6950));
        }
    }
}
