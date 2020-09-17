﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("Data.Models.AuslassModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("BreiteB")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("DurchmesserD")
                        .HasColumnType("REAL");

                    b.Property<double?>("FlaecheA")
                        .HasColumnType("REAL");

                    b.Property<double?>("HoeheH")
                        .HasColumnType("REAL");

                    b.Property<double?>("LaengeL")
                        .HasColumnType("REAL");

                    b.Property<int>("MotorId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("SteuerzeitSZ")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MotorId")
                        .IsUnique();

                    b.ToTable("MotorAuslass");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MotorId = 1
                        },
                        new
                        {
                            Id = 2,
                            MotorId = 2
                        },
                        new
                        {
                            Id = 3,
                            MotorId = 3,
                            SteuerzeitSZ = 148.0
                        },
                        new
                        {
                            Id = 4,
                            MotorId = 4
                        },
                        new
                        {
                            Id = 5,
                            MotorId = 5,
                            SteuerzeitSZ = 145.0
                        },
                        new
                        {
                            Id = 6,
                            MotorId = 6
                        });
                });

            modelBuilder.Entity("Data.Models.AuspuffModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("AbgasT")
                        .HasColumnType("REAL");

                    b.Property<double?>("AbgasV")
                        .HasColumnType("REAL");

                    b.Property<int>("AuslassId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("DiffusorD")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorD1")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorD2")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorD3")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorL")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorL1")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorL2")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorL3")
                        .HasColumnType("REAL");

                    b.Property<int>("DiffusorStage")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("DiffusorW")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorW1")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorW2")
                        .HasColumnType("REAL");

                    b.Property<double?>("DiffusorW3")
                        .HasColumnType("REAL");

                    b.Property<double?>("EndrohrD")
                        .HasColumnType("REAL");

                    b.Property<double?>("EndrohrL")
                        .HasColumnType("REAL");

                    b.Property<double?>("GegenKonusW")
                        .HasColumnType("REAL");

                    b.Property<double?>("GegenkonusD")
                        .HasColumnType("REAL");

                    b.Property<double?>("GegenkonusL")
                        .HasColumnType("REAL");

                    b.Property<double?>("GesamtL")
                        .HasColumnType("REAL");

                    b.Property<double?>("KruemmerD")
                        .HasColumnType("REAL");

                    b.Property<double?>("KruemmerF")
                        .HasColumnType("REAL");

                    b.Property<double?>("KruemmerL")
                        .HasColumnType("REAL");

                    b.Property<double?>("KruemmerW")
                        .HasColumnType("REAL");

                    b.Property<double?>("MittelteilD")
                        .HasColumnType("REAL");

                    b.Property<double?>("MittelteilF")
                        .HasColumnType("REAL");

                    b.Property<double?>("MittelteilL")
                        .HasColumnType("REAL");

                    b.Property<double?>("ResonanzL")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuslassId")
                        .IsUnique();

                    b.ToTable("Auspuff");
                });

            modelBuilder.Entity("Data.Models.AusrollenModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Accuracy")
                        .HasColumnType("REAL");

                    b.Property<double?>("Altitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("AltitudeAccuracy")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DynoId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Heading")
                        .HasColumnType("REAL");

                    b.Property<double?>("HeadingAccuracy")
                        .HasColumnType("REAL");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("Speed")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DynoId");

                    b.ToTable("Ausrollen");
                });

            modelBuilder.Entity("Data.Models.BeschleunigungModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Accuracy")
                        .HasColumnType("REAL");

                    b.Property<double?>("Altitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("AltitudeAccuracy")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DynoId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Heading")
                        .HasColumnType("REAL");

                    b.Property<double?>("HeadingAccuracy")
                        .HasColumnType("REAL");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("Speed")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DynoId");

                    b.ToTable("Beschleunigung");
                });

            modelBuilder.Entity("Data.Models.DrehzahlModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Drehzahl")
                        .HasColumnType("REAL");

                    b.Property<int?>("DynoId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Zeit")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("DynoId");

                    b.ToTable("DynoAudio");
                });

            modelBuilder.Entity("Data.Models.DynoModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Beschreibung")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EnvironmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("Dyno");
                });

            modelBuilder.Entity("Data.Models.DynoNmModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Drehzahl")
                        .HasColumnType("REAL");

                    b.Property<int?>("DynoId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Nm")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DynoId");

                    b.ToTable("DynoNm");
                });

            modelBuilder.Entity("Data.Models.DynoPsModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Drehzahl")
                        .HasColumnType("REAL");

                    b.Property<int?>("DynoId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Ps")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DynoId");

                    b.ToTable("DynoPs");
                });

            modelBuilder.Entity("Data.Models.EinlassModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("BreiteB")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("DurchmesserD")
                        .HasColumnType("REAL");

                    b.Property<double?>("FlaecheA")
                        .HasColumnType("REAL");

                    b.Property<double?>("HoeheH")
                        .HasColumnType("REAL");

                    b.Property<double?>("LaengeL")
                        .HasColumnType("REAL");

                    b.Property<double?>("LuftBedarf")
                        .HasColumnType("REAL");

                    b.Property<int>("MotorId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("SteuerzeitSZ")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MotorId")
                        .IsUnique();

                    b.ToTable("MotorEinlass");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MotorId = 1
                        },
                        new
                        {
                            Id = 2,
                            MotorId = 2
                        },
                        new
                        {
                            Id = 3,
                            MotorId = 3,
                            SteuerzeitSZ = 136.0
                        },
                        new
                        {
                            Id = 4,
                            MotorId = 4
                        },
                        new
                        {
                            Id = 5,
                            MotorId = 5,
                            SteuerzeitSZ = 135.0
                        },
                        new
                        {
                            Id = 6,
                            MotorId = 6
                        });
                });

            modelBuilder.Entity("Data.Models.EnvironmentModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("LuftdruckP")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("TemperaturT")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Environment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LuftdruckP = 1010.0,
                            Name = "Frühling",
                            TemperaturT = 10.0
                        },
                        new
                        {
                            Id = 2,
                            LuftdruckP = 1010.0,
                            Name = "Sommer",
                            TemperaturT = 25.0
                        },
                        new
                        {
                            Id = 3,
                            LuftdruckP = 1010.0,
                            Name = "Herbst",
                            TemperaturT = 10.0
                        },
                        new
                        {
                            Id = 4,
                            LuftdruckP = 1010.0,
                            Name = "Winter",
                            TemperaturT = 1.0
                        });
                });

            modelBuilder.Entity("Data.Models.MotorModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("BohrungD")
                        .HasColumnType("REAL");

                    b.Property<double?>("BrennraumV")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("DeachsierungL")
                        .HasColumnType("REAL");

                    b.Property<double?>("HeizwertU")
                        .HasColumnType("REAL");

                    b.Property<double?>("HubL")
                        .HasColumnType("REAL");

                    b.Property<double?>("HubraumV")
                        .HasColumnType("REAL");

                    b.Property<double?>("KolbenG")
                        .HasColumnType("REAL");

                    b.Property<double?>("KurbelgehaeuseV")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("PleulL")
                        .HasColumnType("REAL");

                    b.Property<double?>("ResonanzU")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("VerdichtungV")
                        .HasColumnType("REAL");

                    b.Property<double?>("Zuendzeitpunkt")
                        .HasColumnType("REAL");

                    b.Property<double?>("ZylinderAnz")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Motor");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BohrungD = 38.0,
                            HubL = 42.0,
                            HubraumV = 47600.0,
                            KurbelgehaeuseV = 142800.0,
                            Name = "Rh 50 II",
                            ResonanzU = 5000.0
                        },
                        new
                        {
                            Id = 2,
                            BohrungD = 38.0,
                            HubL = 42.0,
                            HubraumV = 47600.0,
                            KurbelgehaeuseV = 142800.0,
                            Name = "KRo Rh 50",
                            ResonanzU = 5500.0
                        },
                        new
                        {
                            Id = 3,
                            BohrungD = 40.0,
                            HubL = 39.5,
                            HubraumV = 49600.0,
                            KurbelgehaeuseV = 148800.0,
                            Name = "M53/1 KFR",
                            ResonanzU = 5750.0
                        },
                        new
                        {
                            Id = 4,
                            BohrungD = 40.0,
                            HubL = 39.5,
                            HubraumV = 49600.0,
                            KurbelgehaeuseV = 148800.0,
                            Name = "M 53/21 KF",
                            ResonanzU = 5500.0
                        },
                        new
                        {
                            Id = 5,
                            BohrungD = 38.0,
                            BrennraumV = 5880.0,
                            DeachsierungL = 2.0,
                            HubL = 44.0,
                            HubraumV = 49900.0,
                            KurbelgehaeuseV = 149700.0,
                            Name = "M 541 KF",
                            PleulL = 95.0,
                            ResonanzU = 5500.0
                        },
                        new
                        {
                            Id = 6,
                            BohrungD = 45.0,
                            HubL = 44.0,
                            HubraumV = 49900.0,
                            KurbelgehaeuseV = 209700.0,
                            Name = "M 741/1 KF",
                            ResonanzU = 6000.0
                        });
                });

            modelBuilder.Entity("Data.Models.TuningModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Beschreibung")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("DiagrammeU")
                        .HasColumnType("REAL");

                    b.Property<int?>("EnvironmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("Tuning");
                });

            modelBuilder.Entity("Data.Models.TuningPSModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TuningId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("X")
                        .HasColumnType("REAL");

                    b.Property<double>("Y")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("TuningId");

                    b.ToTable("TuningPs");
                });

            modelBuilder.Entity("Data.Models.UeberstroemerModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Anzahl")
                        .HasColumnType("REAL");

                    b.Property<double?>("BreiteB")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("FlaecheA")
                        .HasColumnType("REAL");

                    b.Property<double?>("HoeheH")
                        .HasColumnType("REAL");

                    b.Property<int>("MotorId")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("SteuerzeitSZ")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MotorId")
                        .IsUnique();

                    b.ToTable("MotorUeberstroemer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MotorId = 1
                        },
                        new
                        {
                            Id = 2,
                            MotorId = 2
                        },
                        new
                        {
                            Id = 3,
                            MotorId = 3,
                            SteuerzeitSZ = 122.0
                        },
                        new
                        {
                            Id = 4,
                            MotorId = 4
                        },
                        new
                        {
                            Id = 5,
                            MotorId = 5,
                            SteuerzeitSZ = 117.0
                        },
                        new
                        {
                            Id = 6,
                            MotorId = 6
                        });
                });

            modelBuilder.Entity("Data.Models.VehiclesModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Beschreibung")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Cw")
                        .HasColumnType("REAL");

                    b.Property<bool>("Deletable")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("FrontA")
                        .HasColumnType("REAL");

                    b.Property<double?>("Gewicht")
                        .HasColumnType("REAL");

                    b.Property<int?>("MotorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Uebersetzung")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MotorId");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Beschreibung = @"Baujahre: 1960 - 1964
Stückzahl: 515.000",
                            Cw = 0.80000000000000004,
                            Deletable = false,
                            FrontA = 0.75,
                            Gewicht = 53.0,
                            MotorId = 1,
                            Name = "SR 2 E"
                        },
                        new
                        {
                            Id = 2,
                            Beschreibung = @"Baujahre: 1959 - 1964
Stückzahl: 164.500",
                            Cw = 0.80000000000000004,
                            Deletable = false,
                            FrontA = 0.75,
                            Gewicht = 73.0,
                            MotorId = 2,
                            Name = "KR 50"
                        },
                        new
                        {
                            Id = 3,
                            Beschreibung = @"Baujahre: 1968 - 1980
Stückzahl: 375.000",
                            Cw = 0.80000000000000004,
                            Deletable = false,
                            FrontA = 0.75,
                            Gewicht = 80.0,
                            MotorId = 3,
                            Name = "KR 51/1 (F)"
                        },
                        new
                        {
                            Id = 4,
                            Beschreibung = @"Baujahre: 1976 - 1980
Stückzahl: 287.000",
                            Cw = 0.80000000000000004,
                            Deletable = false,
                            FrontA = 0.75,
                            Gewicht = 81.0,
                            MotorId = 4,
                            Name = "S 50 B1"
                        },
                        new
                        {
                            Id = 5,
                            Beschreibung = @"Baujahre: 1980 - 1989
Stückzahl: 360.600",
                            Cw = 0.80000000000000004,
                            Deletable = false,
                            FrontA = 0.75,
                            Gewicht = 79.5,
                            MotorId = 5,
                            Name = "S51 B1-4"
                        },
                        new
                        {
                            Id = 6,
                            Beschreibung = @"Baujahre: 1984 - 1988
Stückzahl: 20.000",
                            Cw = 0.80000000000000004,
                            Deletable = false,
                            FrontA = 0.75,
                            Gewicht = 84.0,
                            MotorId = 6,
                            Name = "S 70 C"
                        });
                });

            modelBuilder.Entity("Data.Models.VergaserModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("BenzinLuftF")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("DurchmesserD")
                        .HasColumnType("REAL");

                    b.Property<int>("EinlassId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EinlassId")
                        .IsUnique();

                    b.ToTable("Vergaser");
                });

            modelBuilder.Entity("Data.Models.AuslassModel", b =>
                {
                    b.HasOne("Data.Models.MotorModel", "Motor")
                        .WithOne("Auslass")
                        .HasForeignKey("Data.Models.AuslassModel", "MotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.AuspuffModel", b =>
                {
                    b.HasOne("Data.Models.AuslassModel", "Auslass")
                        .WithOne("Auspuff")
                        .HasForeignKey("Data.Models.AuspuffModel", "AuslassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.AusrollenModel", b =>
                {
                    b.HasOne("Data.Models.DynoModel", "Dyno")
                        .WithMany("Ausrollen")
                        .HasForeignKey("DynoId");
                });

            modelBuilder.Entity("Data.Models.BeschleunigungModel", b =>
                {
                    b.HasOne("Data.Models.DynoModel", "Dyno")
                        .WithMany("Beschleunigung")
                        .HasForeignKey("DynoId");
                });

            modelBuilder.Entity("Data.Models.DrehzahlModel", b =>
                {
                    b.HasOne("Data.Models.DynoModel", "Dyno")
                        .WithMany("Drehzahl")
                        .HasForeignKey("DynoId");
                });

            modelBuilder.Entity("Data.Models.DynoModel", b =>
                {
                    b.HasOne("Data.Models.EnvironmentModel", "Environment")
                        .WithMany("Dyno")
                        .HasForeignKey("EnvironmentId");

                    b.HasOne("Data.Models.VehiclesModel", "Vehicle")
                        .WithOne("Dyno")
                        .HasForeignKey("Data.Models.DynoModel", "VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.DynoNmModel", b =>
                {
                    b.HasOne("Data.Models.DynoModel", "Dyno")
                        .WithMany("DynoNm")
                        .HasForeignKey("DynoId");
                });

            modelBuilder.Entity("Data.Models.DynoPsModel", b =>
                {
                    b.HasOne("Data.Models.DynoModel", "Dyno")
                        .WithMany("DynoPS")
                        .HasForeignKey("DynoId");
                });

            modelBuilder.Entity("Data.Models.EinlassModel", b =>
                {
                    b.HasOne("Data.Models.MotorModel", "Motor")
                        .WithOne("Einlass")
                        .HasForeignKey("Data.Models.EinlassModel", "MotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.TuningModel", b =>
                {
                    b.HasOne("Data.Models.EnvironmentModel", "Environment")
                        .WithMany()
                        .HasForeignKey("EnvironmentId");

                    b.HasOne("Data.Models.VehiclesModel", "Vehicle")
                        .WithOne("Tuning")
                        .HasForeignKey("Data.Models.TuningModel", "VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.TuningPSModel", b =>
                {
                    b.HasOne("Data.Models.TuningModel", "Tuning")
                        .WithMany("Tuning")
                        .HasForeignKey("TuningId");
                });

            modelBuilder.Entity("Data.Models.UeberstroemerModel", b =>
                {
                    b.HasOne("Data.Models.MotorModel", "Motor")
                        .WithOne("Ueberstroemer")
                        .HasForeignKey("Data.Models.UeberstroemerModel", "MotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Models.VehiclesModel", b =>
                {
                    b.HasOne("Data.Models.MotorModel", "Motor")
                        .WithMany("Vehicles")
                        .HasForeignKey("MotorId");
                });

            modelBuilder.Entity("Data.Models.VergaserModel", b =>
                {
                    b.HasOne("Data.Models.EinlassModel", "Einlass")
                        .WithOne("Vergaser")
                        .HasForeignKey("Data.Models.VergaserModel", "EinlassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
