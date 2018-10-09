using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Data.Migrations
{
    public partial class convertGradeEnumToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 1045,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 365, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 1050,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 364, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 2021,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 365, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 2042,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 365, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 3141,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 365, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 4022,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 365, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 4041,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 365, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 364, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 364, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 364, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 1,
                column: "Grade",
                value: "A");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 2,
                column: "Grade",
                value: "C");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 3,
                column: "Grade",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 4,
                column: "Grade",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 5,
                column: "Grade",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 6,
                column: "Grade",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 8,
                column: "Grade",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 9,
                column: "Grade",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 10,
                column: "Grade",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 11,
                column: "Grade",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 358, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 9, 16, 27, 23, 363, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 1045,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 1050,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 247, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 2021,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 2042,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 3141,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 4022,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 4041,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 245, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 1,
                column: "Grade",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 2,
                column: "Grade",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 3,
                column: "Grade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 4,
                column: "Grade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 5,
                column: "Grade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 6,
                column: "Grade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 8,
                column: "Grade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 9,
                column: "Grade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 10,
                column: "Grade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 11,
                column: "Grade",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 236, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local));
        }
    }
}
