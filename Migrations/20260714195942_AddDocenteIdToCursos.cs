using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal_AlvaradoNicole.Migrations
{
    /// <inheritdoc />
    public partial class AddDocenteIdToCursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profesor",
                table: "Cursos");

            migrationBuilder.AddColumn<int>(
                name: "DocenteId",
                table: "Cursos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                column: "DocenteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                column: "DocenteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                column: "DocenteId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_DocenteId",
                table: "Cursos",
                column: "DocenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Docentes_DocenteId",
                table: "Cursos",
                column: "DocenteId",
                principalTable: "Docentes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Docentes_DocenteId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_DocenteId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "DocenteId",
                table: "Cursos");

            migrationBuilder.AddColumn<string>(
                name: "Profesor",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Profesor",
                value: "");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Profesor",
                value: "");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Profesor",
                value: "");
        }
    }
}
