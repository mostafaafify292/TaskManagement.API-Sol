using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTaskFkForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedToId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedToId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AssignedToId1",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "AssignedToId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AspNetUsersId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToId",
                table: "Tasks",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedToId",
                table: "Tasks",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedToId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedToId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AspNetUsersId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedToId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedToId1",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToId1",
                table: "Tasks",
                column: "AssignedToId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignedToId1",
                table: "Tasks",
                column: "AssignedToId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
