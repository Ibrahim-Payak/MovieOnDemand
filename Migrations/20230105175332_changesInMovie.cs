using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieOnDemand.Migrations
{
    public partial class changesInMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CinemaName",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CinemaName",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
