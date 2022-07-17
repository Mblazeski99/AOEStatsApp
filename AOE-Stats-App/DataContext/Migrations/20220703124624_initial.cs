using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataContext.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitStatsItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UnitType = table.Column<int>(type: "INTEGER", nullable: false),
                    Civilization = table.Column<int>(type: "INTEGER", nullable: false),
                    Attributes = table.Column<string>(type: "TEXT", nullable: true),
                    FoodCost = table.Column<double>(type: "REAL", nullable: false),
                    WoodCost = table.Column<double>(type: "REAL", nullable: false),
                    GoldCost = table.Column<double>(type: "REAL", nullable: false),
                    StoneCost = table.Column<double>(type: "REAL", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    VideoLink = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitStatsItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitStatsItems");
        }
    }
}
