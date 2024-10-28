using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fall2024_Assignment3_iqstevens.Data.Migrations
{
    /// <inheritdoc />
    public partial class Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Student_ActorId",
                table: "CourseStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Student_MovieId",
                table: "CourseStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStudent",
                table: "CourseStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Movie");

            migrationBuilder.RenameTable(
                name: "CourseStudent",
                newName: "MovieActor");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Actor");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudent_MovieId",
                table: "MovieActor",
                newName: "IX_MovieActor_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudent_ActorId",
                table: "MovieActor",
                newName: "IX_MovieActor_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieActor",
                table: "MovieActor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Movie_ActorId",
                table: "MovieActor",
                column: "ActorId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Movie_MovieId",
                table: "MovieActor",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Movie_ActorId",
                table: "MovieActor");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Movie_MovieId",
                table: "MovieActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieActor",
                table: "MovieActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "MovieActor",
                newName: "CourseStudent");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Course");

            migrationBuilder.RenameIndex(
                name: "IX_MovieActor_MovieId",
                table: "CourseStudent",
                newName: "IX_CourseStudent_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieActor_ActorId",
                table: "CourseStudent",
                newName: "IX_CourseStudent_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStudent",
                table: "CourseStudent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Student_ActorId",
                table: "CourseStudent",
                column: "ActorId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Student_MovieId",
                table: "CourseStudent",
                column: "MovieId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
