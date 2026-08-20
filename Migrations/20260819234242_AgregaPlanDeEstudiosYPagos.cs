using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoFinal_AlvaradoNicole.Migrations
{
    public partial class AgregaPlanDeEstudiosYPagos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "Cursos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatriculaId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroTransaccion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Id", "Codigo", "Nombre" },
                values: new object[,]
                {
                    { 3, "EN", "Enfermería" },
                    { 4, "DE", "Derecho" },
                    { 5, "ED", "Educación" },
                    { 6, "CO", "Contabilidad" }
                });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Costo",
                value: 180000m);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Costo",
                value: 135000m);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Costo",
                value: 135000m);

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "CarreraId", "Codigo", "Costo", "Creditos", "DocenteId", "Estado", "Horario", "Modalidad", "Nombre", "Sede" },
                values: new object[,]
                {
                    { 4, 1, "SC-310", 180000m, 4, null, "Activo", "Lun/Mié 20:00-22:00", "Presencial", "Programación Avanzada", "San José" },
                    { 5, 1, "SC-315", 135000m, 3, null, "Activo", "Mar/Jue 18:00-20:00", "Presencial", "Redes de Computadoras", "San José" },
                    { 6, 1, "SC-402", 180000m, 4, null, "Activo", "Vie 08:00-12:00", "Virtual", "Ingeniería de Software", "Online" },
                    { 7, 2, "ADM-110", 135000m, 3, null, "Activo", "Lun/Mié 18:00-19:30", "Presencial", "Contabilidad General", "Heredia" },
                    { 8, 2, "ADM-210", 135000m, 3, null, "Activo", "Mar/Jue 19:00-20:30", "Virtual", "Mercadeo", "Online" },
                    { 9, 2, "ADM-310", 180000m, 4, null, "Activo", "Sáb 08:00-12:00", "Presencial", "Finanzas Corporativas", "San José" }
                });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "Estado", "FechaPago", "MatriculaId", "MetodoPago", "Monto", "NumeroTransaccion" },
                values: new object[] { 1, "Completado", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Tarjeta de crédito", 180000m, "TXN-00000001" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "CarreraId", "Codigo", "Costo", "Creditos", "DocenteId", "Estado", "Horario", "Modalidad", "Nombre", "Sede" },
                values: new object[,]
                {
                    { 10, 3, "EN-101", 180000m, 4, null, "Activo", "Lun/Mié 07:00-09:00", "Presencial", "Anatomía y Fisiología", "San José" },
                    { 11, 3, "EN-110", 180000m, 4, null, "Activo", "Mar/Jue 07:00-09:00", "Presencial", "Fundamentos de Enfermería", "San José" },
                    { 12, 3, "EN-201", 135000m, 3, null, "Activo", "Vie 13:00-16:00", "Virtual", "Farmacología", "Online" },
                    { 13, 3, "EN-310", 180000m, 4, null, "Activo", "Lun/Mié 13:00-15:00", "Presencial", "Enfermería Materno-Infantil", "Alajuela" },
                    { 14, 3, "EN-410", 180000m, 4, null, "Activo", "Sáb 07:00-11:00", "Presencial", "Cuidados Críticos", "San José" },
                    { 15, 4, "DE-101", 135000m, 3, null, "Activo", "Lun/Mié 18:00-19:30", "Presencial", "Introducción al Derecho", "San José" },
                    { 16, 4, "DE-110", 180000m, 4, null, "Activo", "Mar/Jue 18:00-20:00", "Presencial", "Derecho Constitucional", "San José" },
                    { 17, 4, "DE-201", 180000m, 4, null, "Activo", "Vie 18:00-21:00", "Virtual", "Derecho Civil I", "Online" },
                    { 18, 4, "DE-301", 180000m, 4, null, "Activo", "Sáb 08:00-12:00", "Presencial", "Derecho Penal", "Heredia" },
                    { 19, 4, "DE-350", 135000m, 3, null, "Activo", "Lun/Mié 20:00-21:30", "Presencial", "Derecho Laboral", "San José" },
                    { 20, 5, "ED-101", 135000m, 3, null, "Activo", "Lun/Mié 17:00-18:30", "Presencial", "Pedagogía General", "San José" },
                    { 21, 5, "ED-150", 135000m, 3, null, "Activo", "Mar/Jue 17:00-18:30", "Virtual", "Psicología del Aprendizaje", "Online" },
                    { 22, 5, "ED-210", 135000m, 3, null, "Activo", "Vie 14:00-17:00", "Presencial", "Didáctica", "Alajuela" },
                    { 23, 5, "ED-310", 135000m, 3, null, "Activo", "Sáb 09:00-12:00", "Virtual", "Evaluación Educativa", "Online" },
                    { 24, 5, "ED-410", 180000m, 4, null, "Activo", "Lun/Mié 08:00-10:00", "Presencial", "Práctica Docente", "San José" },
                    { 25, 6, "CO-101", 180000m, 4, null, "Activo", "Lun/Mié 18:00-20:00", "Presencial", "Contabilidad Básica", "San José" },
                    { 26, 6, "CO-201", 180000m, 4, null, "Activo", "Mar/Jue 18:00-20:00", "Virtual", "Contabilidad Intermedia", "Online" },
                    { 27, 6, "CO-250", 135000m, 3, null, "Activo", "Vie 18:00-21:00", "Presencial", "Costos", "Heredia" },
                    { 28, 6, "CO-310", 135000m, 3, null, "Activo", "Sáb 08:00-11:00", "Presencial", "Auditoría", "San José" },
                    { 29, 6, "CO-350", 135000m, 3, null, "Activo", "Lun/Mié 20:00-21:30", "Virtual", "Impuestos", "Online" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_MatriculaId",
                table: "Pagos",
                column: "MatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_NumeroTransaccion",
                table: "Pagos",
                column: "NumeroTransaccion",
                unique: true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Costo",
                table: "Cursos");
        }
    }
}
