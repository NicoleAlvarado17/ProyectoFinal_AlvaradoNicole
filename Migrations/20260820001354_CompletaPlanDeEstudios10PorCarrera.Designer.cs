using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaMatriculaURA.Models;

#nullable disable

namespace ProyectoFinal_AlvaradoNicole.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260820001354_CompletaPlanDeEstudios10PorCarrera")]
    partial class CompletaPlanDeEstudios10PorCarrera
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Carrera")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Asistencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Presente")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("CursoId", "EstudianteId", "Fecha")
                        .IsUnique();

                    b.ToTable("Asistencias");
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Carrera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carreras");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Codigo = "ING-SIS",
                            Nombre = "Ingeniería en Sistemas"
                        },
                        new
                        {
                            Id = 2,
                            Codigo = "ADM-EMP",
                            Nombre = "Administración de Empresas"
                        },
                        new
                        {
                            Id = 3,
                            Codigo = "EN",
                            Nombre = "Enfermería"
                        },
                        new
                        {
                            Id = 4,
                            Codigo = "DE",
                            Nombre = "Derecho"
                        },
                        new
                        {
                            Id = 5,
                            Codigo = "ED",
                            Nombre = "Educación"
                        },
                        new
                        {
                            Id = 6,
                            Codigo = "CO",
                            Nombre = "Contabilidad"
                        });
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarreraId")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Costo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Creditos")
                        .HasColumnType("int");

                    b.Property<int?>("DocenteId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modalidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sede")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarreraId");

                    b.HasIndex("DocenteId");

                    b.ToTable("Cursos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarreraId = 1,
                            Codigo = "SC-301",
                            Costo = 180000m,
                            Creditos = 4,
                            DocenteId = 1,
                            Estado = "Activo",
                            Horario = "Lun/Mié 18:00-20:00",
                            Modalidad = "Presencial",
                            Nombre = "Estructuras de Datos",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 2,
                            CarreraId = 1,
                            Codigo = "SC-220",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Mar/Jue 19:00-21:00",
                            Modalidad = "Virtual",
                            Nombre = "Bases de Datos II",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 4,
                            CarreraId = 1,
                            Codigo = "SC-310",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Lun/Mié 20:00-22:00",
                            Modalidad = "Presencial",
                            Nombre = "Programación Avanzada",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 5,
                            CarreraId = 1,
                            Codigo = "SC-315",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Mar/Jue 18:00-20:00",
                            Modalidad = "Presencial",
                            Nombre = "Redes de Computadoras",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 6,
                            CarreraId = 1,
                            Codigo = "SC-402",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Vie 08:00-12:00",
                            Modalidad = "Virtual",
                            Nombre = "Ingeniería de Software",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 3,
                            CarreraId = 2,
                            Codigo = "ADM-101",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Vie 08:00-11:00",
                            Modalidad = "Presencial",
                            Nombre = "Introducción a la Administración",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 7,
                            CarreraId = 2,
                            Codigo = "ADM-110",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Lun/Mié 18:00-19:30",
                            Modalidad = "Presencial",
                            Nombre = "Contabilidad General",
                            Sede = "Heredia"
                        },
                        new
                        {
                            Id = 8,
                            CarreraId = 2,
                            Codigo = "ADM-210",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Mar/Jue 19:00-20:30",
                            Modalidad = "Virtual",
                            Nombre = "Mercadeo",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 9,
                            CarreraId = 2,
                            Codigo = "ADM-310",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Sáb 08:00-12:00",
                            Modalidad = "Presencial",
                            Nombre = "Finanzas Corporativas",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 10,
                            CarreraId = 3,
                            Codigo = "EN-101",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Lun/Mié 07:00-09:00",
                            Modalidad = "Presencial",
                            Nombre = "Anatomía y Fisiología",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 11,
                            CarreraId = 3,
                            Codigo = "EN-110",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Mar/Jue 07:00-09:00",
                            Modalidad = "Presencial",
                            Nombre = "Fundamentos de Enfermería",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 12,
                            CarreraId = 3,
                            Codigo = "EN-201",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Vie 13:00-16:00",
                            Modalidad = "Virtual",
                            Nombre = "Farmacología",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 13,
                            CarreraId = 3,
                            Codigo = "EN-310",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Lun/Mié 13:00-15:00",
                            Modalidad = "Presencial",
                            Nombre = "Enfermería Materno-Infantil",
                            Sede = "Alajuela"
                        },
                        new
                        {
                            Id = 14,
                            CarreraId = 3,
                            Codigo = "EN-410",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Sáb 07:00-11:00",
                            Modalidad = "Presencial",
                            Nombre = "Cuidados Críticos",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 15,
                            CarreraId = 4,
                            Codigo = "DE-101",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Lun/Mié 18:00-19:30",
                            Modalidad = "Presencial",
                            Nombre = "Introducción al Derecho",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 16,
                            CarreraId = 4,
                            Codigo = "DE-110",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Mar/Jue 18:00-20:00",
                            Modalidad = "Presencial",
                            Nombre = "Derecho Constitucional",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 17,
                            CarreraId = 4,
                            Codigo = "DE-201",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Vie 18:00-21:00",
                            Modalidad = "Virtual",
                            Nombre = "Derecho Civil I",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 18,
                            CarreraId = 4,
                            Codigo = "DE-301",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Sáb 08:00-12:00",
                            Modalidad = "Presencial",
                            Nombre = "Derecho Penal",
                            Sede = "Heredia"
                        },
                        new
                        {
                            Id = 19,
                            CarreraId = 4,
                            Codigo = "DE-350",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Lun/Mié 20:00-21:30",
                            Modalidad = "Presencial",
                            Nombre = "Derecho Laboral",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 20,
                            CarreraId = 5,
                            Codigo = "ED-101",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Lun/Mié 17:00-18:30",
                            Modalidad = "Presencial",
                            Nombre = "Pedagogía General",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 21,
                            CarreraId = 5,
                            Codigo = "ED-150",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Mar/Jue 17:00-18:30",
                            Modalidad = "Virtual",
                            Nombre = "Psicología del Aprendizaje",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 22,
                            CarreraId = 5,
                            Codigo = "ED-210",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Vie 14:00-17:00",
                            Modalidad = "Presencial",
                            Nombre = "Didáctica",
                            Sede = "Alajuela"
                        },
                        new
                        {
                            Id = 23,
                            CarreraId = 5,
                            Codigo = "ED-310",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Sáb 09:00-12:00",
                            Modalidad = "Virtual",
                            Nombre = "Evaluación Educativa",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 24,
                            CarreraId = 5,
                            Codigo = "ED-410",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Lun/Mié 08:00-10:00",
                            Modalidad = "Presencial",
                            Nombre = "Práctica Docente",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 25,
                            CarreraId = 6,
                            Codigo = "CO-101",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Lun/Mié 18:00-20:00",
                            Modalidad = "Presencial",
                            Nombre = "Contabilidad Básica",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 26,
                            CarreraId = 6,
                            Codigo = "CO-201",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Mar/Jue 18:00-20:00",
                            Modalidad = "Virtual",
                            Nombre = "Contabilidad Intermedia",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 27,
                            CarreraId = 6,
                            Codigo = "CO-250",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Vie 18:00-21:00",
                            Modalidad = "Presencial",
                            Nombre = "Costos",
                            Sede = "Heredia"
                        },
                        new
                        {
                            Id = 28,
                            CarreraId = 6,
                            Codigo = "CO-310",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Sáb 08:00-11:00",
                            Modalidad = "Presencial",
                            Nombre = "Auditoría",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 29,
                            CarreraId = 6,
                            Codigo = "CO-350",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Lun/Mié 20:00-21:30",
                            Modalidad = "Virtual",
                            Nombre = "Impuestos",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 30,
                            CarreraId = 1,
                            Codigo = "SC-410",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Sáb 13:00-17:00",
                            Modalidad = "Híbrido",
                            Nombre = "Inteligencia Artificial",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 31,
                            CarreraId = 1,
                            Codigo = "SC-215",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Lun/Mié 07:00-09:00",
                            Modalidad = "Presencial",
                            Nombre = "Sistemas Operativos",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 32,
                            CarreraId = 1,
                            Codigo = "SC-150",
                            Costo = 90000m,
                            Creditos = 2,
                            Estado = "Activo",
                            Horario = "Mar/Jue 07:00-08:30",
                            Modalidad = "Virtual",
                            Nombre = "Introducción a la Programación",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 33,
                            CarreraId = 2,
                            Codigo = "ADM-320",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Vie 17:00-20:00",
                            Modalidad = "Híbrido",
                            Nombre = "Gestión de Proyectos",
                            Sede = "Heredia"
                        },
                        new
                        {
                            Id = 34,
                            CarreraId = 2,
                            Codigo = "ADM-150",
                            Costo = 90000m,
                            Creditos = 2,
                            Estado = "Activo",
                            Horario = "Sáb 08:00-10:00",
                            Modalidad = "Presencial",
                            Nombre = "Matemática Financiera",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 35,
                            CarreraId = 2,
                            Codigo = "ADM-410",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Lun/Mié 19:00-21:00",
                            Modalidad = "Virtual",
                            Nombre = "Comercio Internacional",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 36,
                            CarreraId = 3,
                            Codigo = "EN-150",
                            Costo = 90000m,
                            Creditos = 2,
                            Estado = "Activo",
                            Horario = "Mar 14:00-16:00",
                            Modalidad = "Virtual",
                            Nombre = "Nutrición Clínica",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 37,
                            CarreraId = 3,
                            Codigo = "EN-320",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Jue 13:00-16:00",
                            Modalidad = "Híbrido",
                            Nombre = "Salud Pública",
                            Sede = "Alajuela"
                        },
                        new
                        {
                            Id = 38,
                            CarreraId = 3,
                            Codigo = "EN-450",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Sáb 13:00-17:00",
                            Modalidad = "Presencial",
                            Nombre = "Enfermería Quirúrgica",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 39,
                            CarreraId = 4,
                            Codigo = "DE-150",
                            Costo = 90000m,
                            Creditos = 2,
                            Estado = "Activo",
                            Horario = "Mar 18:00-19:30",
                            Modalidad = "Presencial",
                            Nombre = "Derecho Romano",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 40,
                            CarreraId = 4,
                            Codigo = "DE-410",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Vie 18:00-21:00",
                            Modalidad = "Híbrido",
                            Nombre = "Derecho Internacional",
                            Sede = "Heredia"
                        },
                        new
                        {
                            Id = 41,
                            CarreraId = 4,
                            Codigo = "DE-320",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Jue 18:00-20:30",
                            Modalidad = "Virtual",
                            Nombre = "Derecho Mercantil",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 42,
                            CarreraId = 5,
                            Codigo = "ED-160",
                            Costo = 90000m,
                            Creditos = 2,
                            Estado = "Activo",
                            Horario = "Mar 16:00-17:30",
                            Modalidad = "Virtual",
                            Nombre = "Tecnología Educativa",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 43,
                            CarreraId = 5,
                            Codigo = "ED-320",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Sáb 09:00-12:00",
                            Modalidad = "Híbrido",
                            Nombre = "Gestión de Aula",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 44,
                            CarreraId = 5,
                            Codigo = "ED-450",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Lun/Mié 15:00-17:00",
                            Modalidad = "Presencial",
                            Nombre = "Necesidades Educativas Especiales",
                            Sede = "Alajuela"
                        },
                        new
                        {
                            Id = 45,
                            CarreraId = 6,
                            Codigo = "CO-150",
                            Costo = 90000m,
                            Creditos = 2,
                            Estado = "Activo",
                            Horario = "Mar 07:00-08:30",
                            Modalidad = "Presencial",
                            Nombre = "Matemática Financiera Contable",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 46,
                            CarreraId = 6,
                            Codigo = "CO-410",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Sáb 13:00-17:00",
                            Modalidad = "Híbrido",
                            Nombre = "Contabilidad de Costos Avanzada",
                            Sede = "Heredia"
                        },
                        new
                        {
                            Id = 47,
                            CarreraId = 6,
                            Codigo = "CO-320",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Jue 19:00-21:00",
                            Modalidad = "Virtual",
                            Nombre = "Normas Internacionales (NIIF)",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 48,
                            CarreraId = 1,
                            Codigo = "SC-330",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Jue 18:00-20:00",
                            Modalidad = "Presencial",
                            Nombre = "Arquitectura de Software",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 49,
                            CarreraId = 1,
                            Codigo = "SC-360",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Sáb 09:00-11:00",
                            Modalidad = "Virtual",
                            Nombre = "Seguridad Informática",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 50,
                            CarreraId = 2,
                            Codigo = "ADM-220",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Mar 18:00-19:30",
                            Modalidad = "Presencial",
                            Nombre = "Recursos Humanos",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 51,
                            CarreraId = 2,
                            Codigo = "ADM-350",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Jue 17:00-19:30",
                            Modalidad = "Híbrido",
                            Nombre = "Logística y Cadena de Suministro",
                            Sede = "Heredia"
                        },
                        new
                        {
                            Id = 52,
                            CarreraId = 2,
                            Codigo = "ADM-450",
                            Costo = 90000m,
                            Creditos = 2,
                            Estado = "Activo",
                            Horario = "Vie 19:00-20:30",
                            Modalidad = "Virtual",
                            Nombre = "Emprendimiento",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 53,
                            CarreraId = 3,
                            Codigo = "EN-250",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Mié 14:00-16:30",
                            Modalidad = "Presencial",
                            Nombre = "Salud Mental",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 54,
                            CarreraId = 3,
                            Codigo = "EN-360",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Vie 09:00-11:30",
                            Modalidad = "Híbrido",
                            Nombre = "Enfermería Comunitaria",
                            Sede = "Alajuela"
                        },
                        new
                        {
                            Id = 55,
                            CarreraId = 4,
                            Codigo = "DE-360",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Mié 18:00-20:30",
                            Modalidad = "Presencial",
                            Nombre = "Derecho Administrativo",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 56,
                            CarreraId = 4,
                            Codigo = "DE-420",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Sáb 09:00-13:00",
                            Modalidad = "Virtual",
                            Nombre = "Derecho Tributario",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 57,
                            CarreraId = 5,
                            Codigo = "ED-250",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Mié 17:00-19:30",
                            Modalidad = "Presencial",
                            Nombre = "Currículo Educativo",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 58,
                            CarreraId = 5,
                            Codigo = "ED-360",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Vie 16:00-18:30",
                            Modalidad = "Virtual",
                            Nombre = "Educación Inclusiva",
                            Sede = "Online"
                        },
                        new
                        {
                            Id = 59,
                            CarreraId = 6,
                            Codigo = "CO-260",
                            Costo = 135000m,
                            Creditos = 3,
                            Estado = "Activo",
                            Horario = "Jue 18:00-20:30",
                            Modalidad = "Presencial",
                            Nombre = "Contabilidad Gubernamental",
                            Sede = "San José"
                        },
                        new
                        {
                            Id = 60,
                            CarreraId = 6,
                            Codigo = "CO-420",
                            Costo = 180000m,
                            Creditos = 4,
                            Estado = "Activo",
                            Horario = "Sáb 09:00-13:00",
                            Modalidad = "Híbrido",
                            Nombre = "Auditoría Avanzada",
                            Sede = "Heredia"
                        });
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Docente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Docentes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Correo = "docente1@ura.com",
                            Especialidad = "Bases de Datos",
                            Nombre = "Carlos Brenes Solano"
                        });
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Estudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarreraId")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarreraId");

                    b.ToTable("Estudiantes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarreraId = 1,
                            Correo = "estudiante1@ura.com",
                            Nombre = "Nicole Vargas Solano"
                        });
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Matricula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("Matriculas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CursoId = 1,
                            Estado = "Activa",
                            EstudianteId = 1,
                            Fecha = new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Pago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<int>("MatriculaId")
                        .HasColumnType("int");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumeroTransaccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MatriculaId");

                    b.HasIndex("NumeroTransaccion")
                        .IsUnique();

                    b.ToTable("Pagos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Estado = "Completado",
                            FechaPago = new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MatriculaId = 1,
                            MetodoPago = "Tarjeta de crédito",
                            Monto = 180000m,
                            NumeroTransaccion = "TXN-00000001"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SistemaMatriculaURA.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SistemaMatriculaURA.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaMatriculaURA.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SistemaMatriculaURA.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Asistencia", b =>
                {
                    b.HasOne("SistemaMatriculaURA.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SistemaMatriculaURA.Models.Estudiante", "Estudiante")
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Curso", b =>
                {
                    b.HasOne("SistemaMatriculaURA.Models.Carrera", "Carrera")
                        .WithMany()
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaMatriculaURA.Models.Docente", "Docente")
                        .WithMany()
                        .HasForeignKey("DocenteId");

                    b.Navigation("Carrera");

                    b.Navigation("Docente");
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Estudiante", b =>
                {
                    b.HasOne("SistemaMatriculaURA.Models.Carrera", "Carrera")
                        .WithMany()
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrera");
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Matricula", b =>
                {
                    b.HasOne("SistemaMatriculaURA.Models.Curso", "Curso")
                        .WithMany("Matriculas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SistemaMatriculaURA.Models.Estudiante", "Estudiante")
                        .WithMany("Matriculas")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Pago", b =>
                {
                    b.HasOne("SistemaMatriculaURA.Models.Matricula", "Matricula")
                        .WithMany()
                        .HasForeignKey("MatriculaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Matricula");
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Curso", b =>
                {
                    b.Navigation("Matriculas");
                });

            modelBuilder.Entity("SistemaMatriculaURA.Models.Estudiante", b =>
                {
                    b.Navigation("Matriculas");
                });
#pragma warning restore 612, 618
        }
    }
}
