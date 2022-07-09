using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockChatroom.Infrastructure.Data.Migrations;

public partial class UpdateChatRoom : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "ChatRooms",
            keyColumn: "Id",
            keyValue: new Guid("380a2770-2935-4d93-9321-87d9e7549592"));

        migrationBuilder.InsertData(
            table: "ChatRooms",
            columns: new[] { "Id", "Name" },
            values: new object[] { new Guid("a822dc7b-ae6e-4d66-8397-a93362074471"), "Main Chat" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "ChatRooms",
            keyColumn: "Id",
            keyValue: new Guid("a822dc7b-ae6e-4d66-8397-a93362074471"));

        migrationBuilder.InsertData(
            table: "ChatRooms",
            columns: new[] { "Id", "Name" },
            values: new object[] { new Guid("380a2770-2935-4d93-9321-87d9e7549592"), "Main Chat" });
    }
}
