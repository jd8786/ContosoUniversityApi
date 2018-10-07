using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Data.Migrations
{
    public partial class initialTheData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instructor",
                columns: new[] { "InstructorId", "CreatedBy", "CreatedDate", "FirstMidName", "HireDate", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), "Kim", new DateTime(1995, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abercrombie", null, null },
                    { 2, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), "Fadi", new DateTime(2002, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fakhouri", null, null },
                    { 3, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), "Roger", new DateTime(1998, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harui", null, null },
                    { 4, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), "Candace", new DateTime(2001, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kapoor", null, null },
                    { 5, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), "Roger", new DateTime(2004, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zheng", null, null }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "CreatedBy", "CreatedDate", "EnrollmentDate", "FirstMidName", "LastName", "OriginCountry", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 236, DateTimeKind.Local), new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carson", "Alexander", "USA", null, null },
                    { 2, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meredith", "Alonso", "UK", null, null },
                    { 3, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), new DateTime(2013, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arturo", "Anand", "TURKEY", null, null },
                    { 4, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gytis", "Barzdukas", "CHINA", null, null },
                    { 5, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yan", "Li", "JAPAN", null, null },
                    { 6, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), new DateTime(2011, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peggy", "Justice", "UK", null, null },
                    { 7, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), new DateTime(2013, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laura", "Norman", "USA", null, null },
                    { 8, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), new DateTime(2005, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nino", "Olivetto", "RUSSIA", null, null }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "Budget", "CreatedBy", "CreatedDate", "InstructorId", "Name", "StartDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 350000m, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 245, DateTimeKind.Local), 1, "English", new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 2, 100000m, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local), 2, "Mathematics", new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 3, 350000m, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local), 3, "Engineering", new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 4, 100000m, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local), 4, "Economics", new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "OfficeAssignment",
                columns: new[] { "InstructorId", "Location" },
                values: new object[,]
                {
                    { 2, "Smith 17" },
                    { 3, "Gowan 27" },
                    { 4, "Thompson 304" }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseId", "CreatedBy", "CreatedDate", "Credits", "DepartmentId", "Title", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 2021, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), 3, 1, "Composition", null, null },
                    { 2042, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), 4, 1, "Literature", null, null },
                    { 1045, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), 4, 2, "Calculus", null, null },
                    { 3141, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), 4, 2, "Trigonometry", null, null },
                    { 1050, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 247, DateTimeKind.Local), 3, 3, "Chemistry", null, null },
                    { 4022, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), 3, 4, "Microeconomics", null, null },
                    { 4041, "ContosoUniversityUsers", new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), 3, 4, "Macroeconomics", null, null }
                });

            migrationBuilder.InsertData(
                table: "CourseAssignment",
                columns: new[] { "CourseAssignmentId", "CourseId", "InstructorId" },
                values: new object[,]
                {
                    { 7, 2021, 1 },
                    { 3, 4022, 5 },
                    { 2, 1050, 3 },
                    { 4, 4041, 5 },
                    { 6, 3141, 3 },
                    { 1, 1050, 4 },
                    { 8, 2042, 1 },
                    { 5, 1045, 2 }
                });

            migrationBuilder.InsertData(
                table: "Enrollment",
                columns: new[] { "EnrollmentId", "CourseId", "Grade", "StudentId" },
                values: new object[,]
                {
                    { 4, 1045, 1, 2 },
                    { 11, 2042, 1, 6 },
                    { 5, 3141, 1, 2 },
                    { 10, 2021, 1, 5 },
                    { 1, 1050, 0, 1 },
                    { 7, 1050, null, 3 },
                    { 9, 1050, 1, 4 },
                    { 6, 2021, 1, 2 },
                    { 2, 4022, 2, 1 },
                    { 8, 4022, 1, 3 },
                    { 3, 4041, 1, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseAssignment",
                keyColumn: "CourseAssignmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourseAssignment",
                keyColumn: "CourseAssignmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CourseAssignment",
                keyColumn: "CourseAssignmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CourseAssignment",
                keyColumn: "CourseAssignmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseAssignment",
                keyColumn: "CourseAssignmentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseAssignment",
                keyColumn: "CourseAssignmentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CourseAssignment",
                keyColumn: "CourseAssignmentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CourseAssignment",
                keyColumn: "CourseAssignmentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Enrollment",
                keyColumn: "EnrollmentId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OfficeAssignment",
                keyColumn: "InstructorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OfficeAssignment",
                keyColumn: "InstructorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfficeAssignment",
                keyColumn: "InstructorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 2042);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 3141);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 4022);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseId",
                keyValue: 4041);

            migrationBuilder.DeleteData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "DepartmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructor",
                keyColumn: "InstructorId",
                keyValue: 4);
        }
    }
}
