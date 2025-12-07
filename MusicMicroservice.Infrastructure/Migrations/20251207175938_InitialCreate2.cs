using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicExecutors_Musics_MusicId",
                table: "MusicExecutors");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicExecutors_Musics_MusicId",
                table: "MusicExecutors",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicExecutors_Musics_MusicId",
                table: "MusicExecutors");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicExecutors_Musics_MusicId",
                table: "MusicExecutors",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
