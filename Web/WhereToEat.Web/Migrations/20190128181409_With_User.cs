using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WhereToEat.Web.Migrations
{
    public partial class With_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "GroupId", "GroupId1", "Password", "Salt", "Username" },
                values: new object[] { new Guid("0d28fead-09fc-462a-93b1-a6231342533f"), "ptjuanramos@gmail.com", null, null, "5EgWkNpFtw0+sDQ34L+kmovNc5HudBCClecGaqeG/LM=", "WmHoyqwbh/s4Wbn047W9Nw==", "ptjuanramos@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0d28fead-09fc-462a-93b1-a6231342533f"));
        }
    }
}
