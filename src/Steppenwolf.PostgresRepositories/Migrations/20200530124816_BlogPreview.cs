using Microsoft.EntityFrameworkCore.Migrations;

namespace Steppenwolf.PostgresRepositories.Migrations
{
    public partial class BlogPreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Preview",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preview",
                table: "Blogs");
        }
    }
}
