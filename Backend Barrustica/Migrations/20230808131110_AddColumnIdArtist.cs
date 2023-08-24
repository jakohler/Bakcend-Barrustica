using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Barrustica.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnIdArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdArtist",
                table: "PieceEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdArtist",
                table: "PieceEntity");
        }
    }
}
