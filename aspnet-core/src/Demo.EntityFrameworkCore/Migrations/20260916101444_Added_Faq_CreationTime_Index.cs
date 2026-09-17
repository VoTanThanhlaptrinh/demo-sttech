using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class Added_Faq_CreationTime_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppFrequentlyAskedQuestions_CreationTime",
                table: "AppFrequentlyAskedQuestions",
                column: "CreationTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppFrequentlyAskedQuestions_CreationTime",
                table: "AppFrequentlyAskedQuestions");
        }
    }
}
