using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoFinal_AlvaradoNicole.Migrations
{
    public partial class AgregaHistorialAcademicoYHorarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cuatrimestre",
                table: "Matriculas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Nota",
                table: "Matriculas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Aula",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Grupo",
                table: "Cursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Asistencias",
                columns: new[] { "Id", "CursoId", "EstudianteId", "Fecha", "Presente" },
                values: new object[,]
                {
                    { 9001, 1, 1, new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9002, 1, 1, new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9003, 1, 1, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9004, 1, 1, new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 9005, 2, 1, new DateTime(2026, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9006, 2, 1, new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9007, 2, 1, new DateTime(2026, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9008, 2, 1, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9009, 2, 1, new DateTime(2026, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 9010, 30, 1, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9011, 30, 1, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 9012, 49, 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9013, 49, 1, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9014, 49, 1, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "Aula 204", "A" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Aula", "DocenteId", "Grupo" },
                values: new object[] { "Sala Virtual 1", 1, "B" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Aula", "DocenteId", "Grupo" },
                values: new object[] { "Sala Virtual 2", 1, "A" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Aula", "DocenteId", "Grupo" },
                values: new object[] { "Aula 210", 1, "C" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Aula", "DocenteId", "Grupo" },
                values: new object[] { "Sala Virtual 3", 1, "B" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "Aula", "Grupo" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cuatrimestre", "Nota" },
                values: new object[] { "II-2026", null });

            migrationBuilder.InsertData(
                table: "Matriculas",
                columns: new[] { "Id", "Cuatrimestre", "CursoId", "Estado", "EstudianteId", "Fecha", "Nota" },
                values: new object[,]
                {
                    { 9001, "II-2026", 2, "Activa", 1, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 9002, "II-2026", 30, "Activa", 1, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 9003, "II-2026", 49, "Activa", 1, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 9004, "I-2025", 32, "Aprobada", 1, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 92 },
                    { 9005, "I-2025", 4, "Aprobada", 1, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 78 },
                    { 9006, "II-2025", 5, "Reprobada", 1, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 65 },
                    { 9007, "II-2025", 48, "Aprobada", 1, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85 },
                    { 9008, "II-2025", 6, "Aprobada", 1, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 90 },
                    { 9009, "I-2026", 31, "Aprobada", 1, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 88 }
                });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "Estado", "FechaPago", "MatriculaId", "MetodoPago", "Monto", "NumeroTransaccion" },
                values: new object[,]
                {
                    { 9001, "Completado", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9001, "Tarjeta de crédito", 135000m, "TXN-00009001" },
                    { 9002, "Completado", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9002, "Tarjeta de crédito", 180000m, "TXN-00009002" },
                    { 9003, "Completado", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9003, "Tarjeta de crédito", 135000m, "TXN-00009003" }
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9001);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9002);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9003);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9004);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9005);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9006);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9007);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9008);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9009);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9010);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9011);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9012);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9013);

            migrationBuilder.DeleteData(
                table: "Asistencias",
                keyColumn: "Id",
                keyValue: 9014);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9004);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9005);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9006);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9007);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9008);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9009);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9001);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9002);

            migrationBuilder.DeleteData(
                table: "Pagos",
                keyColumn: "Id",
                keyValue: 9003);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9001);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9002);

            migrationBuilder.DeleteData(
                table: "Matriculas",
                keyColumn: "Id",
                keyValue: 9003);

            migrationBuilder.DropColumn(
                name: "Cuatrimestre",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "Nota",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "Aula",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Grupo",
                table: "Cursos");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                column: "DocenteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 6,
                column: "DocenteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 31,
                column: "DocenteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 49,
                column: "DocenteId",
                value: null);
        }
    }
}
