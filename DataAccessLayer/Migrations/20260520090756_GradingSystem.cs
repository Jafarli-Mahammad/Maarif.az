using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class GradingSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubmissionFile",
                table: "SubmissionFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submission",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentFile",
                table: "AssignmentFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "SubmissionFile",
                newName: "SubmissionFiles");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "AssignmentFile",
                newName: "AssignmentFiles");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assignments");

            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                table: "Lessons",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubmissionFiles",
                table: "SubmissionFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentFiles",
                table: "AssignmentFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    HasLaboratory = table.Column<bool>(type: "bit", nullable: false),
                    ManualPracticalScore = table.Column<int>(type: "int", nullable: true),
                    FreelanceWork = table.Column<int>(type: "int", nullable: true),
                    MidtermScore = table.Column<int>(type: "int", nullable: true),
                    LaboratoryScore = table.Column<int>(type: "int", nullable: true),
                    ExamScore = table.Column<int>(type: "int", nullable: true),
                    AttendanceScore = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LetterGrade = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    GradePointValue = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: true),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FinalizedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradingScales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LetterGrade = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MinScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    MaxScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    GradePoint = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradingScales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    LectureDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false, defaultValue: 90),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TranscriptRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectCategory = table.Column<int>(type: "int", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    PreExamTotal = table.Column<int>(type: "int", nullable: false),
                    ExamScore = table.Column<int>(type: "int", nullable: true),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    LetterGrade = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    GradePoint = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                    IsRetake = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsPassed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FinalizedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    FinalizedByTeacherId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranscriptRecords", x => x.Id);
                    table.CheckConstraint("CK_TranscriptRecord_GradePoint_Range", "[GradePoint] >= 0 AND [GradePoint] <= 4.0");
                    table.CheckConstraint("CK_TranscriptRecord_Scores_NonNegative", "[PreExamTotal] >= 0 AND [TotalScore] >= 0 AND [Credits] >= 0");
                    table.ForeignKey(
                        name: "FK_TranscriptRecords_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TranscriptRecords_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LectureMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LectureId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DownloadCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureMaterials_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GradingScales",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "GradePoint", "LastModifiedAt", "LastModifiedBy", "LetterGrade", "MaxScore", "MinScore" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, null, "Əla", 4.0m, null, null, "A", 100m, 91m },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, null, "Yaxşı", 3.0m, null, null, "B", 90m, 71m },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, null, "Kafi", 2.0m, null, null, "C", 70m, 51m },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, null, "Qeyri-kafi", 1.0m, null, null, "D", 50m, 31m },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, null, "Zəif", 0.0m, null, null, "F", 30m, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LessonId",
                table: "Assignments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_LessonId",
                table: "Grades",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId_LessonId",
                table: "Grades",
                columns: new[] { "StudentId", "LessonId" },
                unique: true,
                filter: "[DeletedAt] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GradingScales_LetterGrade",
                table: "GradingScales",
                column: "LetterGrade",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LectureMaterials_LectureId",
                table: "LectureMaterials",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LessonId",
                table: "Lectures",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LessonId_OrderIndex",
                table: "Lectures",
                columns: new[] { "LessonId", "OrderIndex" },
                unique: true,
                filter: "[DeletedAt] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptRecords_LessonId",
                table: "TranscriptRecords",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptRecords_StudentId_AcademicYear_Semester",
                table: "TranscriptRecords",
                columns: new[] { "StudentId", "AcademicYear", "Semester" });

            migrationBuilder.CreateIndex(
                name: "IX_TranscriptRecords_StudentId_LessonId_AcademicYear_Semester",
                table: "TranscriptRecords",
                columns: new[] { "StudentId", "LessonId", "AcademicYear", "Semester" },
                unique: true,
                filter: "[DeletedAt] IS NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Lessons_LessonId",
                table: "Assignments",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Lessons_LessonId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "GradingScales");

            migrationBuilder.DropTable(
                name: "LectureMaterials");

            migrationBuilder.DropTable(
                name: "PracticalRecords");

            migrationBuilder.DropTable(
                name: "TranscriptRecords");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubmissionFiles",
                table: "SubmissionFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_LessonId",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentFiles",
                table: "AssignmentFiles");

            migrationBuilder.DropColumn(
                name: "AcademicYear",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameTable(
                name: "SubmissionFiles",
                newName: "SubmissionFile");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Assignment");

            migrationBuilder.RenameTable(
                name: "AssignmentFiles",
                newName: "AssignmentFile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submission",
                table: "Submission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubmissionFile",
                table: "SubmissionFile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentFile",
                table: "AssignmentFile",
                column: "Id");
        }
    }
}
