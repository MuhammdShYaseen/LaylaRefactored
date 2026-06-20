using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Layla.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexGeoLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            CREATE SPATIAL INDEX IX_Apartments_Coordinates
            ON Apartments(Coordinates)
            USING GEOGRAPHY_AUTO_GRID;
            """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
            DROP INDEX IX_Apartments_Coordinates
            ON Apartments;
            """);
        }
    }
}
