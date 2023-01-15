using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieOnDemand.Migrations
{
    public partial class RenamePropInProducer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Producers",
                newName: "ProducerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "Producers",
                newName: "Id");
        }
    }
}
