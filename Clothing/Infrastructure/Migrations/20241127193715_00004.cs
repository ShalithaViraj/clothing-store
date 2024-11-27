using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _00004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatedByUserId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UpdatedByUserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatedByUserId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UpdatedByUserId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "User",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "User",
                newName: "CreateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "User",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "User",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedByUserId",
                table: "User",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedByUserId",
                table: "User",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatedByUserId",
                table: "User",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UpdatedByUserId",
                table: "User",
                column: "UpdatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
