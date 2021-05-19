using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_.NET.Migrations
{
    public partial class recipes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Recipes");
        }
    }
}
