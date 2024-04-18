using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QuizItems",
                columns: new[] { "Id", "CorrectAnswer", "Question" },
                values: new object[,]
                {
                    { 5, "15", "5 * 3" },
                    { 6, "1", "2 : 2" }
                });

            migrationBuilder.InsertData(
                table: "QuizEntityQuizItemEntity",
                columns: new[] { "ItemsId", "QuizzesId" },
                values: new object[,]
                {
                    { 5, 2 },
                    { 6, 2 }
                });

            migrationBuilder.InsertData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                columns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                values: new object[,]
                {
                    { 2, 5 },
                    { 2, 6 },
                    { 6, 5 },
                    { 6, 6 },
                    { 8, 5 },
                    { 8, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuizEntityQuizItemEntity",
                keyColumns: new[] { "ItemsId", "QuizzesId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "QuizEntityQuizItemEntity",
                keyColumns: new[] { "ItemsId", "QuizzesId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "QuizItemAnswerEntityQuizItemEntity",
                keyColumns: new[] { "IncorrectAnswersId", "QuizItemsId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "QuizItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "QuizItems",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
