using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal_AlvaradoNicole.Migrations
{
    public partial class NombreMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Presente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asistencias_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencias_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.InsertData(
                table: "Docentes",
                columns: new[] { "Id", "Correo", "Especialidad", "Nombre" },
                values: new object[] { 1, "docente1@ura.com", "Bases de Datos", "Carlos Brenes Solano" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DocenteId", "Estado", "Horario", "Modalidad", "Sede" },
                values: new object[] { 1, "Activo", "Lun/Mié 18:00-20:00", "Presencial", "San José" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Estado", "Horario", "Modalidad", "Sede" },
                values: new object[] { "Activo", "Mar/Jue 19:00-21:00", "Virtual", "Online" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Estado", "Horario", "Modalidad", "Sede" },
                values: new object[] { "Activo", "Vie 08:00-11:00", "Presencial", "San José" });

            migrationBuilder.InsertData(
                table: "Estudiantes",
                columns: new[] { "Id", "CarreraId", "Correo", "Nombre", "UserId" },
                values: new object[] { 1, 1, "estudiante1@ura.com", "Nicole Vargas Solano", null });

            migrationBuilder.InsertData(
                table: "Matriculas",
                columns: new[] { "Id", "CursoId", "Estado", "EstudianteId", "Fecha" },
                values: new object[] { 1, 1, "Activa", 1, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_CursoId_EstudianteId_Fecha",
                table: "Asistencias",
                columns: new[] { "CursoId", "EstudianteId", "Fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_EstudianteId",
                table: "Asistencias",
                column: "EstudianteId");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estudiantes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Cursos");
            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DocenteId", "Horario", "Modalidad", "Sede" },
                values: new object[] { null, "", "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Horario", "Modalidad", "Sede" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Horario", "Modalidad", "Sede" },
                values: new object[] { "", "", "" });

            migrationBuilder.DeleteData(
                table: "Docentes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
