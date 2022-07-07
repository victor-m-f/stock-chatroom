using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockChatroom.Infrastructure.Data.Migrations;

public partial class SeedChatRoom : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "ChatRooms",
            columns: new[] { "Id", "Name" },
            values: new object[] { new Guid("380a2770-2935-4d93-9321-87d9e7549592"), "Main Chat" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "ChatRooms",
            keyColumn: "Id",
            keyValue: new Guid("380a2770-2935-4d93-9321-87d9e7549592"));
    }
}
