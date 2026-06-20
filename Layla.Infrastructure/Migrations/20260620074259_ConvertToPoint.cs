using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Layla.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConvertToPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Apartments");

            migrationBuilder.AddColumn<Point>(
                name: "Coordinates",
                table: "Apartments",
                type: "geography",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Apartments");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Apartments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Apartments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
