using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryRelationWithTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Categories_CategoryId",
                table: "Tasks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Categories_CategoryId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
