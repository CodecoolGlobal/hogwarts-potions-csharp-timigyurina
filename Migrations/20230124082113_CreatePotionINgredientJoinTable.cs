using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogwartsPotions.Migrations
{
    /// <inheritdoc />
    public partial class CreatePotionINgredientJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PotionIngredients",
                columns: table => new
                {
                    PotionId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotionIngredients", x => new { x.PotionId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_PotionIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PotionIngredients_Potions_PotionId",
                        column: x => x.PotionId,
                        principalTable: "Potions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PotionIngredients_IngredientId",
                table: "PotionIngredients",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PotionIngredients");
        }
    }
}
