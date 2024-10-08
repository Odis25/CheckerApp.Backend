﻿// <auto-generated />
using System;
using CheckerApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CheckerApp.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CheckerApp.Domain.Entities.CheckEntities.CheckParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HardwareCheckId")
                        .HasColumnType("int");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Result")
                        .HasColumnType("bit");

                    b.Property<int?>("SoftwareCheckId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HardwareCheckId");

                    b.HasIndex("SoftwareCheckId");

                    b.ToTable("CheckParameters");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.CheckEntities.HardwareCheck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HardwareId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HardwareId")
                        .IsUnique();

                    b.ToTable("HardwareChecks");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.CheckEntities.SoftwareCheck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SoftwareId")
                        .IsUnique();

                    b.ToTable("SoftwareChecks");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.ContractEntities.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContractNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DomesticNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasProtocol")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Hardware", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("Hardwares");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.SoftwareEntities.Software", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoftwareType")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("Softwares");

                    b.HasDiscriminator<int>("SoftwareType").HasValue(1);
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.APC", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("APCs");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.ARM", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<bool>("HasRAID")
                        .HasColumnType("bit");

                    b.Property<string>("Keyboard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyboardSN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Monitor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Monitor2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Monitor2SN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonitorSN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mouse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MouseSN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductKeyOS")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("ARMs");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Cabinet", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<DateTime>("Constructed")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConstructedBy")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Cabinets");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.DiffPressure", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MaxValue")
                        .HasColumnType("float");

                    b.Property<double>("MinValue")
                        .HasColumnType("float");

                    b.Property<int>("SignalType")
                        .HasColumnType("int");

                    b.ToTable("DiffPressures");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.FireModule", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FireModules");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.FireSensor", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FireSensors");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.FlowComputer", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("AssemblyVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CRC32")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("LastConfigDate")
                        .HasColumnType("decimal(20,0)");

                    b.ToTable("FlowComputers");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Flowmeter", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Kfactor")
                        .HasColumnType("float");

                    b.Property<double>("MaxValue")
                        .HasColumnType("float");

                    b.Property<double>("MinValue")
                        .HasColumnType("float");

                    b.Property<int>("SignalType")
                        .HasColumnType("int");

                    b.ToTable("Flowmeters");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.GasAnalyzer", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MaxValue")
                        .HasColumnType("float");

                    b.Property<double>("MinValue")
                        .HasColumnType("float");

                    b.Property<int>("SignalType")
                        .HasColumnType("int");

                    b.ToTable("GasAnalyzers");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.InformPanel", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PanelType")
                        .HasColumnType("int");

                    b.ToTable("InformPanels");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.NetworkHardware", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mask")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("NetworkHardwares");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.PLC", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("AssemblyVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("PLCs");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Pressure", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MaxValue")
                        .HasColumnType("float");

                    b.Property<double>("MinValue")
                        .HasColumnType("float");

                    b.Property<int>("SignalType")
                        .HasColumnType("int");

                    b.ToTable("Pressures");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Temperature", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MaxValue")
                        .HasColumnType("float");

                    b.Property<double>("MinValue")
                        .HasColumnType("float");

                    b.Property<int>("SignalType")
                        .HasColumnType("int");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Valve", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.HardwareEntities.Hardware");

                    b.Property<string>("DeviceModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SignalType")
                        .HasColumnType("int");

                    b.ToTable("Valves");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.SoftwareEntities.SCADA", b =>
                {
                    b.HasBaseType("CheckerApp.Domain.Entities.SoftwareEntities.Software");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.CheckEntities.CheckParameter", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.CheckEntities.HardwareCheck", null)
                        .WithMany("CheckParameters")
                        .HasForeignKey("HardwareCheckId");

                    b.HasOne("CheckerApp.Domain.Entities.CheckEntities.SoftwareCheck", null)
                        .WithMany("CheckParameters")
                        .HasForeignKey("SoftwareCheckId");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.CheckEntities.HardwareCheck", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", "Hardware")
                        .WithOne("CheckResult")
                        .HasForeignKey("CheckerApp.Domain.Entities.CheckEntities.HardwareCheck", "HardwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hardware");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.CheckEntities.SoftwareCheck", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.SoftwareEntities.Software", "Software")
                        .WithOne("CheckResult")
                        .HasForeignKey("CheckerApp.Domain.Entities.CheckEntities.SoftwareCheck", "SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Software");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Hardware", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.ContractEntities.Contract", null)
                        .WithMany("HardwareList")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.SoftwareEntities.Software", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.ContractEntities.Contract", null)
                        .WithMany("SoftwareList")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.APC", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.APC", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.ARM", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.ARM", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsMany("CheckerApp.Domain.Entities.HardwareEntities.NetworkAdapter", "NetworkAdapters", b1 =>
                        {
                            b1.Property<int>("ARMId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("IP")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("MacAddress")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ARMId", "Id");

                            b1.ToTable("NetworkAdapters");

                            b1.WithOwner()
                                .HasForeignKey("ARMId");
                        });

                    b.Navigation("NetworkAdapters");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Cabinet", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.Cabinet", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.DiffPressure", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.DiffPressure", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.FireModule", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.FireModule", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.FireSensor", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.FireSensor", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.FlowComputer", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.FlowComputer", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Flowmeter", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.Flowmeter", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsOne("CheckerApp.Domain.Entities.HardwareEntities.ModbusSettings", "Settings", b1 =>
                        {
                            b1.Property<int>("FlowmeterId")
                                .HasColumnType("int");

                            b1.Property<long>("Address")
                                .HasColumnType("bigint");

                            b1.Property<string>("BoudRate")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("DataBits")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Parity")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StopBit")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("FlowmeterId");

                            b1.ToTable("FlowmeterSettings");

                            b1.WithOwner()
                                .HasForeignKey("FlowmeterId");
                        });

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.GasAnalyzer", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.GasAnalyzer", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.InformPanel", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.InformPanel", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.NetworkHardware", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.NetworkHardware", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsMany("CheckerApp.Domain.Entities.HardwareEntities.NetworkDevice", "NetworkDevices", b1 =>
                        {
                            b1.Property<int>("NetworkHardwareId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("IP")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("MacAddress")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("NetworkHardwareId", "Id");

                            b1.ToTable("NetworkDevices");

                            b1.WithOwner()
                                .HasForeignKey("NetworkHardwareId");
                        });

                    b.Navigation("NetworkDevices");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.PLC", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.PLC", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Pressure", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.Pressure", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Temperature", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.Temperature", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Valve", b =>
                {
                    b.HasOne("CheckerApp.Domain.Entities.HardwareEntities.Hardware", null)
                        .WithOne()
                        .HasForeignKey("CheckerApp.Domain.Entities.HardwareEntities.Valve", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsOne("CheckerApp.Domain.Entities.HardwareEntities.ModbusSettings", "Settings", b1 =>
                        {
                            b1.Property<int>("ValveId")
                                .HasColumnType("int");

                            b1.Property<long>("Address")
                                .HasColumnType("bigint");

                            b1.Property<string>("BoudRate")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("DataBits")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Parity")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StopBit")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ValveId");

                            b1.ToTable("ValveSettings");

                            b1.WithOwner()
                                .HasForeignKey("ValveId");
                        });

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.CheckEntities.HardwareCheck", b =>
                {
                    b.Navigation("CheckParameters");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.CheckEntities.SoftwareCheck", b =>
                {
                    b.Navigation("CheckParameters");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.ContractEntities.Contract", b =>
                {
                    b.Navigation("HardwareList");

                    b.Navigation("SoftwareList");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.HardwareEntities.Hardware", b =>
                {
                    b.Navigation("CheckResult");
                });

            modelBuilder.Entity("CheckerApp.Domain.Entities.SoftwareEntities.Software", b =>
                {
                    b.Navigation("CheckResult");
                });
#pragma warning restore 612, 618
        }
    }
}
