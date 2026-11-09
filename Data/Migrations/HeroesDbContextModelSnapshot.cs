using System;
using HeroesFluent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HeroesFluent.Data.Migrations
{
    [DbContext(typeof(HeroesDbContext))]
    partial class HeroesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HeroesFluent.Models.Equipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaFundacion")
                        .HasColumnType("date");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<decimal>("Presupuesto")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Equipos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FechaFundacion = new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Guardianes",
                            Presupuesto = 250000.00m
                        },
                        new
                        {
                            Id = 2,
                            FechaFundacion = new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Vigilantes del Sur",
                            Presupuesto = 120500.50m
                        });
                });

            modelBuilder.Entity("HeroesFluent.Models.Heroe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int?>("EquipoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("NivelPoder")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Alias")
                        .IsUnique();

                    b.HasIndex("EquipoId");

                    b.ToTable("Heroes", null, t =>
                        {
                            t.HasCheckConstraint("CK_Heroes_NivelPoder", "[NivelPoder] BETWEEN 1 AND 100");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "Centella",
                            EquipoId = 1,
                            FechaRegistro = new DateTime(2026, 1, 10, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            NivelPoder = 85,
                            Nombre = "Ana Torres"
                        },
                        new
                        {
                            Id = 2,
                            Alias = "Sombra",
                            EquipoId = 1,
                            FechaRegistro = new DateTime(2026, 2, 3, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            NivelPoder = 72,
                            Nombre = "Bruno Díaz"
                        },
                        new
                        {
                            Id = 3,
                            Alias = "Tormenta",
                            EquipoId = 2,
                            FechaRegistro = new DateTime(2026, 3, 21, 11, 15, 0, 0, DateTimeKind.Unspecified),
                            NivelPoder = 90,
                            Nombre = "Carla Méndez"
                        },
                        new
                        {
                            Id = 4,
                            Alias = "Eco",
                            FechaRegistro = new DateTime(2026, 5, 8, 16, 45, 0, 0, DateTimeKind.Unspecified),
                            NivelPoder = 40,
                            Nombre = "Diego Ruiz"
                        });
                });

            modelBuilder.Entity("HeroesFluent.Models.HeroeMision", b =>
                {
                    b.Property<int>("HeroeId")
                        .HasColumnType("int");

                    b.Property<int>("MisionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaAsignacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("HeroeId", "MisionId");

                    b.HasIndex("MisionId");

                    b.ToTable("HeroeMisiones", (string)null);

                    b.HasData(
                        new
                        {
                            HeroeId = 1,
                            MisionId = 1,
                            FechaAsignacion = new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rol = "Líder"
                        },
                        new
                        {
                            HeroeId = 2,
                            MisionId = 1,
                            FechaAsignacion = new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rol = "Apoyo"
                        },
                        new
                        {
                            HeroeId = 1,
                            MisionId = 2,
                            FechaAsignacion = new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rol = "Apoyo"
                        },
                        new
                        {
                            HeroeId = 3,
                            MisionId = 2,
                            FechaAsignacion = new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rol = "Líder"
                        },
                        new
                        {
                            HeroeId = 3,
                            MisionId = 3,
                            FechaAsignacion = new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rol = "Líder"
                        });
                });

            modelBuilder.Entity("HeroesFluent.Models.Mision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Dificultad")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("FechaLimite")
                        .HasColumnType("date");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Misiones", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Evacuar a los ciudadanos en peligro.",
                            Dificultad = "Baja",
                            FechaLimite = new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Rescate en la ciudad"
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Proteger la ciudad de una invasión.",
                            Dificultad = "Alta",
                            FechaLimite = new DateTime(2026, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Detener la invasión"
                        },
                        new
                        {
                            Id = 3,
                            Dificultad = "Extrema",
                            Titulo = "Defender el planeta"
                        });
                });


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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");
                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");
                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");
                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");
                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");
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

            modelBuilder.Entity("HeroesFluent.Models.Heroe", b =>
                {
                    b.HasOne("HeroesFluent.Models.Equipo", "Equipo")
                        .WithMany("Heroes")
                        .HasForeignKey("EquipoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Equipo");
                });

            modelBuilder.Entity("HeroesFluent.Models.HeroeMision", b =>
                {
                    b.HasOne("HeroesFluent.Models.Heroe", "Heroe")
                        .WithMany("HeroeMisiones")
                        .HasForeignKey("HeroeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeroesFluent.Models.Mision", "Mision")
                        .WithMany("HeroeMisiones")
                        .HasForeignKey("MisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Heroe");

                    b.Navigation("Mision");
                });

            modelBuilder.Entity("HeroesFluent.Models.Equipo", b =>
                {
                    b.Navigation("Heroes");
                });

            modelBuilder.Entity("HeroesFluent.Models.Heroe", b =>
                {
                    b.Navigation("HeroeMisiones");
                });

            modelBuilder.Entity("HeroesFluent.Models.Mision", b =>
                {
                    b.Navigation("HeroeMisiones");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

#pragma warning restore 612, 618
        }
    }
}
