using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicMicroservice.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Executors",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FirstName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                LastName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Nickname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Executors", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Musics",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Year = table.Column<int>(type: "integer", nullable: false),
                Style = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Musics", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MusicExecutors",
            columns: table => new
            {
                ExecutorId = table.Column<Guid>(type: "uuid", nullable: false),
                MusicId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MusicExecutors", x => new { x.ExecutorId, x.MusicId });
                table.ForeignKey(
                    name: "FK_MusicExecutors_Executors_ExecutorId",
                    column: x => x.ExecutorId,
                    principalTable: "Executors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_MusicExecutors_Musics_MusicId",
                    column: x => x.MusicId,
                    principalTable: "Musics",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_MusicExecutors_MusicId",
            table: "MusicExecutors",
            column: "MusicId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MusicExecutors");

        migrationBuilder.DropTable(
            name: "Executors");

        migrationBuilder.DropTable(
            name: "Musics");
    }
}
