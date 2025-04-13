using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyRecipesVectors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipesVectors_Recipes_RecipeId",
                table: "RecipesVectors");

            migrationBuilder.DropIndex(
                name: "IX_RecipesVectors_RecipeId",
                table: "RecipesVectors");

            migrationBuilder.AddColumn<string>(
                name: "Recipe",
                table: "RecipesVectors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recipe",
                table: "RecipesVectors");

            migrationBuilder.CreateIndex(
                name: "IX_RecipesVectors_RecipeId",
                table: "RecipesVectors",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesVectors_Recipes_RecipeId",
                table: "RecipesVectors",
                column: "RecipeId",
                principalSchema: "recipes",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
