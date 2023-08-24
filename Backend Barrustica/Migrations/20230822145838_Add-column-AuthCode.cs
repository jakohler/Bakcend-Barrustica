using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Barrustica.Migrations
{
    /// <inheritdoc />
    public partial class AddcolumnAuthCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthCode",
                table: "UserEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthCode",
                table: "UserEntity");
        }
    }
}
