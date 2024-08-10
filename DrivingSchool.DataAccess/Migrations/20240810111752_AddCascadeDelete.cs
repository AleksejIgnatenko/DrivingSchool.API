using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchool.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerUserTests_Tests_TestId",
                table: "AnswerUserTests");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerUserTests_Users_UserId",
                table: "AnswerUserTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Categories_CategoryId",
                table: "Tests");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerUserTests_Tests_TestId",
                table: "AnswerUserTests",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerUserTests_Users_UserId",
                table: "AnswerUserTests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Categories_CategoryId",
                table: "Tests",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerUserTests_Tests_TestId",
                table: "AnswerUserTests");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerUserTests_Users_UserId",
                table: "AnswerUserTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Categories_CategoryId",
                table: "Tests");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerUserTests_Tests_TestId",
                table: "AnswerUserTests",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerUserTests_Users_UserId",
                table: "AnswerUserTests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Categories_CategoryId",
                table: "Tests",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
