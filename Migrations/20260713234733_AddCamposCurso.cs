using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal_AlvaradoNicole.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposCurso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Horario",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Modalidad",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profesor",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sede",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Horario", "Modalidad", "Profesor", "Sede" },
                values: new object[] { "", "", "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Horario", "Modalidad", "Profesor", "Sede" },
                values: new object[] { "", "", "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Horario", "Modalidad", "Profesor", "Sede" },
                values: new object[] { "", "", "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Horario",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Modalidad",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Profesor",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Sede",
                table: "Cursos");
        }
    }
}
