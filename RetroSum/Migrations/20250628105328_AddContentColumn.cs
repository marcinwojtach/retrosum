using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroSum.Migrations
{
    /// <inheritdoc />
    public partial class AddContentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Retros",
                type: "TEXT",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Retros");
        }
    }
}
