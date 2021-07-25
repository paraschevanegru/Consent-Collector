using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsentCollector.Persistence.Migrations
{
    public partial class CheckConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddCheckConstraint(
                name: "CK_UserDetail_Firstname",
                table: "UserDetail",
                sql: "Len([Firstname])>=3 and Len([Firstname])<=20");

            migrationBuilder.AddCheckConstraint(
                name: "CK_UserDetail_Lastname",
                table: "UserDetail",
                sql: "Len([Lastname])>=3 and Len([Lastname])<=20");

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_Role",
                table: "User",
                sql: "[Role] = 'admin' or [Role] = 'user'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Survey_LegalBasis",
                table: "Survey",
                sql: "[LegalBasis] = 'Contract' or [LegalBasis] = 'Law' or [LegalBasis] = 'Legitimate Interest'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Question_Questions",
                table: "Question",
                sql: "Len([Questions])>=5 and Len([Questions])<=100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Comment_Text",
                table: "Comment",
                sql: "Len([Text])>=5 and Len([Text])<=100");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_UserDetail_Firstname",
                table: "UserDetail");

            migrationBuilder.DropCheckConstraint(
                name: "CK_UserDetail_Lastname",
                table: "UserDetail");

            migrationBuilder.DropCheckConstraint(
                name: "CK_User_Role",
                table: "User");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Survey_LegalBasis",
                table: "Survey");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Question_Questions",
                table: "Question");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Comment_Text",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Notification");
        }
    }
}
