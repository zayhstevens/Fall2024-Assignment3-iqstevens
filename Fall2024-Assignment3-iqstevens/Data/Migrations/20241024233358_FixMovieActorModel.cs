using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fall2024_Assignment3_iqstevens.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMovieActorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Movie_ActorId",
                table: "MovieActor");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Actor_ActorId",
                table: "MovieActor",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Actor_ActorId",
                table: "MovieActor");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Movie_ActorId",
                table: "MovieActor",
                column: "ActorId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
