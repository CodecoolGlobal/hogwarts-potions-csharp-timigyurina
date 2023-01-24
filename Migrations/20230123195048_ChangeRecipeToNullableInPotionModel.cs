using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogwartsPotions.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRecipeToNullableInPotionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Potions_Recipes_RecipeId",
                table: "Potions");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "Potions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Potions_Recipes_RecipeId",
                table: "Potions",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Potions_Recipes_RecipeId",
                table: "Potions");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "Potions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Potions_Recipes_RecipeId",
                table: "Potions",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
