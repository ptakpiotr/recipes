using Microsoft.EntityFrameworkCore.Migrations;
using Pgvector;

#nullable disable

namespace Recipes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecipesVectors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Vector>(
                name: "Vector",
                table: "RecipesVectors",
                type: "vector(1024)",
                nullable: false,
                oldClrType: typeof(Vector),
                oldType: "vector(512)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Vector>(
                name: "Vector",
                table: "RecipesVectors",
                type: "vector(512)",
                nullable: false,
                oldClrType: typeof(Vector),
                oldType: "vector(1024)");
        }
    }
}
