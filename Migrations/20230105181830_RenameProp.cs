using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieOnDemand.Migrations
{
    public partial class RenameProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "Producers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CinemaId",
                table: "Cinemas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ActorId",
                table: "Actors",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Producers",
                newName: "ProducerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Movies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cinemas",
                newName: "CinemaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Actors",
                newName: "ActorId");
        }
    }
}
