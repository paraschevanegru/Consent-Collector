using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsentCollector.Persistence.Migrations
{
    public partial class MigrationV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSurvey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdQuestion = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestion_Question_IdQuestion",
                        column: x => x.IdQuestion,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyQuestion_Survey_IdSurvey",
                        column: x => x.IdSurvey,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_IdQuestion",
                table: "SurveyQuestion",
                column: "IdQuestion");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_IdSurvey",
                table: "SurveyQuestion",
                column: "IdSurvey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyQuestion");
        }
    }
}
