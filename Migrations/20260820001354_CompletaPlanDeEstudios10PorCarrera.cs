using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoFinal_AlvaradoNicole.Migrations
{
    public partial class CompletaPlanDeEstudios10PorCarrera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "CarreraId", "Codigo", "Costo", "Creditos", "DocenteId", "Estado", "Horario", "Modalidad", "Nombre", "Sede" },
                values: new object[,]
                {
                    { 30, 1, "SC-410", 180000m, 4, null, "Activo", "Sáb 13:00-17:00", "Híbrido", "Inteligencia Artificial", "San José" },
                    { 31, 1, "SC-215", 135000m, 3, null, "Activo", "Lun/Mié 07:00-09:00", "Presencial", "Sistemas Operativos", "San José" },
                    { 32, 1, "SC-150", 90000m, 2, null, "Activo", "Mar/Jue 07:00-08:30", "Virtual", "Introducción a la Programación", "Online" },
                    { 33, 2, "ADM-320", 135000m, 3, null, "Activo", "Vie 17:00-20:00", "Híbrido", "Gestión de Proyectos", "Heredia" },
                    { 34, 2, "ADM-150", 90000m, 2, null, "Activo", "Sáb 08:00-10:00", "Presencial", "Matemática Financiera", "San José" },
                    { 35, 2, "ADM-410", 180000m, 4, null, "Activo", "Lun/Mié 19:00-21:00", "Virtual", "Comercio Internacional", "Online" },
                    { 36, 3, "EN-150", 90000m, 2, null, "Activo", "Mar 14:00-16:00", "Virtual", "Nutrición Clínica", "Online" },
                    { 37, 3, "EN-320", 135000m, 3, null, "Activo", "Jue 13:00-16:00", "Híbrido", "Salud Pública", "Alajuela" },
                    { 38, 3, "EN-450", 180000m, 4, null, "Activo", "Sáb 13:00-17:00", "Presencial", "Enfermería Quirúrgica", "San José" },
                    { 39, 4, "DE-150", 90000m, 2, null, "Activo", "Mar 18:00-19:30", "Presencial", "Derecho Romano", "San José" },
                    { 40, 4, "DE-410", 180000m, 4, null, "Activo", "Vie 18:00-21:00", "Híbrido", "Derecho Internacional", "Heredia" },
                    { 41, 4, "DE-320", 135000m, 3, null, "Activo", "Jue 18:00-20:30", "Virtual", "Derecho Mercantil", "Online" },
                    { 42, 5, "ED-160", 90000m, 2, null, "Activo", "Mar 16:00-17:30", "Virtual", "Tecnología Educativa", "Online" },
                    { 43, 5, "ED-320", 135000m, 3, null, "Activo", "Sáb 09:00-12:00", "Híbrido", "Gestión de Aula", "San José" },
                    { 44, 5, "ED-450", 180000m, 4, null, "Activo", "Lun/Mié 15:00-17:00", "Presencial", "Necesidades Educativas Especiales", "Alajuela" },
                    { 45, 6, "CO-150", 90000m, 2, null, "Activo", "Mar 07:00-08:30", "Presencial", "Matemática Financiera Contable", "San José" },
                    { 46, 6, "CO-410", 180000m, 4, null, "Activo", "Sáb 13:00-17:00", "Híbrido", "Contabilidad de Costos Avanzada", "Heredia" },
                    { 47, 6, "CO-320", 135000m, 3, null, "Activo", "Jue 19:00-21:00", "Virtual", "Normas Internacionales (NIIF)", "Online" },
                    { 48, 1, "SC-330", 135000m, 3, null, "Activo", "Jue 18:00-20:00", "Presencial", "Arquitectura de Software", "San José" },
                    { 49, 1, "SC-360", 135000m, 3, null, "Activo", "Sáb 09:00-11:00", "Virtual", "Seguridad Informática", "Online" },
                    { 50, 2, "ADM-220", 135000m, 3, null, "Activo", "Mar 18:00-19:30", "Presencial", "Recursos Humanos", "San José" },
                    { 51, 2, "ADM-350", 135000m, 3, null, "Activo", "Jue 17:00-19:30", "Híbrido", "Logística y Cadena de Suministro", "Heredia" },
                    { 52, 2, "ADM-450", 90000m, 2, null, "Activo", "Vie 19:00-20:30", "Virtual", "Emprendimiento", "Online" },
                    { 53, 3, "EN-250", 135000m, 3, null, "Activo", "Mié 14:00-16:30", "Presencial", "Salud Mental", "San José" },
                    { 54, 3, "EN-360", 135000m, 3, null, "Activo", "Vie 09:00-11:30", "Híbrido", "Enfermería Comunitaria", "Alajuela" },
                    { 55, 4, "DE-360", 135000m, 3, null, "Activo", "Mié 18:00-20:30", "Presencial", "Derecho Administrativo", "San José" },
                    { 56, 4, "DE-420", 180000m, 4, null, "Activo", "Sáb 09:00-13:00", "Virtual", "Derecho Tributario", "Online" },
                    { 57, 5, "ED-250", 135000m, 3, null, "Activo", "Mié 17:00-19:30", "Presencial", "Currículo Educativo", "San José" },
                    { 58, 5, "ED-360", 135000m, 3, null, "Activo", "Vie 16:00-18:30", "Virtual", "Educación Inclusiva", "Online" },
                    { 59, 6, "CO-260", 135000m, 3, null, "Activo", "Jue 18:00-20:30", "Presencial", "Contabilidad Gubernamental", "San José" },
                    { 60, 6, "CO-420", 180000m, 4, null, "Activo", "Sáb 09:00-13:00", "Híbrido", "Auditoría Avanzada", "Heredia" }
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 60);
        }
    }
}
