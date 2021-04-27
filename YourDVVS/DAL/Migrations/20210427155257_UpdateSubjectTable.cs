using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateSubjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjects_lecturers_lecturer_id",
                table: "subjects");

            migrationBuilder.AlterColumn<int>(
                name: "max_number_of_students",
                table: "subjects",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "subject_lecturer_user_id",
                table: "subjects",
                column: "lecturer_id",
                principalTable: "lecturers",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "subject_lecturer_user_id",
                table: "subjects");

            migrationBuilder.AlterColumn<int>(
                name: "max_number_of_students",
                table: "subjects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_lecturers_lecturer_id",
                table: "subjects",
                column: "lecturer_id",
                principalTable: "lecturers",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
