using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddFKinBesAusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ausrollen_Dyno_DynoId",
                table: "Ausrollen");

            migrationBuilder.DropForeignKey(
                name: "FK_Beschleunigung_Dyno_DynoId",
                table: "Beschleunigung");

            migrationBuilder.AlterColumn<int>(
                name: "DynoId",
                table: "Beschleunigung",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DynoId",
                table: "Ausrollen",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ausrollen_Dyno_DynoId",
                table: "Ausrollen",
                column: "DynoId",
                principalTable: "Dyno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beschleunigung_Dyno_DynoId",
                table: "Beschleunigung",
                column: "DynoId",
                principalTable: "Dyno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ausrollen_Dyno_DynoId",
                table: "Ausrollen");

            migrationBuilder.DropForeignKey(
                name: "FK_Beschleunigung_Dyno_DynoId",
                table: "Beschleunigung");

            migrationBuilder.AlterColumn<int>(
                name: "DynoId",
                table: "Beschleunigung",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DynoId",
                table: "Ausrollen",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Ausrollen_Dyno_DynoId",
                table: "Ausrollen",
                column: "DynoId",
                principalTable: "Dyno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Beschleunigung_Dyno_DynoId",
                table: "Beschleunigung",
                column: "DynoId",
                principalTable: "Dyno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
