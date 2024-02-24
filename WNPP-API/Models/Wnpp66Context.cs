using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WNPP_API.Models;

public partial class Wnpp66Context : DbContext
{
    public Wnpp66Context()
    {
    }

    public Wnpp66Context(DbContextOptions<Wnpp66Context> options)
        : base(options)
    {
    }

    public virtual DbSet<MDropDown> MDropDowns { get; set; }

    public virtual DbSet<TAccount> TAccounts { get; set; }

    public virtual DbSet<TBook> TBooks { get; set; }

    public virtual DbSet<TBooksInventory> TBooksInventories { get; set; }

    public virtual DbSet<TBranch> TBranches { get; set; }

    public virtual DbSet<TBranchRegister> TBranchRegisters { get; set; }

    public virtual DbSet<TMonk> TMonks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=localhost;Database=WNPP66;UID=wnpp;PWD=P@55w0rd;TrustServerCertificate=True;MultipleActiveResultSets=true;");
        => optionsBuilder.UseSqlServer("Server=192.168.1.99;Database=WNPP66;UID=wnpp;PWD=P@55w0rd;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CS_AI");

        modelBuilder.Entity<MDropDown>(entity =>
        {
            entity.ToTable("M_DropDown");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DropDownId).HasColumnName("DropDownID");
            entity.Property(e => e.DropDownName).HasMaxLength(250);
            entity.Property(e => e.DropDownType).HasMaxLength(50);
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.ModifiedByName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Notation).HasMaxLength(500);
            entity.Property(e => e.RecordStatus).HasMaxLength(10);
        });

        modelBuilder.Entity<TAccount>(entity =>
        {
            entity.ToTable("T_Account");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName).HasMaxLength(250);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMail");
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Notation).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.RecordStatus).HasMaxLength(10);
            entity.Property(e => e.Ref1).HasMaxLength(250);
            entity.Property(e => e.Ref2).HasMaxLength(250);
        });

        modelBuilder.Entity<TBook>(entity =>
        {
            entity.ToTable("T_Books");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.BookName).HasMaxLength(250);
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Edition).HasMaxLength(20);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.ModifiedByName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Notation).HasMaxLength(500);
            entity.Property(e => e.PublicationDate)
                .HasColumnType("datetime")
                .HasColumnName("Publication_date");
            entity.Property(e => e.Publisher).HasMaxLength(100);
            entity.Property(e => e.RecordStatus).HasMaxLength(10);
        });

        modelBuilder.Entity<TBooksInventory>(entity =>
        {
            entity.ToTable("T_BooksInventory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.BookName).HasMaxLength(250);
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedByName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.MoveDate).HasColumnType("datetime");
            entity.Property(e => e.Notation).HasMaxLength(500);
            entity.Property(e => e.RecordStatus).HasMaxLength(10);
        });

        modelBuilder.Entity<TBranch>(entity =>
        {
            entity.ToTable("T_Branch");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AbbotEmail)
                .HasMaxLength(50)
                .HasColumnName("AbbotEMail");
            entity.Property(e => e.AbbotImageUrl).HasMaxLength(250);
            entity.Property(e => e.AbbotLineId)
                .HasMaxLength(50)
                .HasColumnName("AbbotLineID");
            entity.Property(e => e.AbbotName).HasMaxLength(250);
            entity.Property(e => e.AbbotPhoneNo)
                .HasMaxLength(50)
                .HasColumnName("AbbotPhoneNO");
            entity.Property(e => e.AbbotSignDate).HasColumnType("datetime");
            entity.Property(e => e.AbbotTemple).HasMaxLength(250);
            entity.Property(e => e.AbbotTitle).HasMaxLength(50);
            entity.Property(e => e.AddressTextMonatery)
                .HasMaxLength(500)
                .HasColumnName("AddressText_Monatery");
            entity.Property(e => e.BranchName).HasMaxLength(250);
            entity.Property(e => e.BranchTypeName).HasMaxLength(50);
            entity.Property(e => e.CertifierName).HasMaxLength(250);
            entity.Property(e => e.CertifierTemple).HasMaxLength(250);
            entity.Property(e => e.CountryMonatery)
                .HasMaxLength(100)
                .HasColumnName("Country_Monatery");
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfAcceptPosition).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DateOfFounding).HasColumnType("datetime");
            entity.Property(e => e.DateOfOrdination).HasColumnType("datetime");
            entity.Property(e => e.DepositaryName).HasMaxLength(50);
            entity.Property(e => e.DepositaryPhoneNo)
                .HasMaxLength(50)
                .HasColumnName("DepositaryPhoneNO");
            entity.Property(e => e.DistrictMonatery)
                .HasMaxLength(100)
                .HasColumnName("District_Monatery");
            entity.Property(e => e.EcclesiasticalTitle).HasMaxLength(50);
            entity.Property(e => e.HouseNoMonatery)
                .HasMaxLength(50)
                .HasColumnName("HouseNo_Monatery");
            entity.Property(e => e.LandRightsDocuments).HasMaxLength(50);
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.ModifiedByName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.MonasteryAreaNgan).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MonasteryAreaRai).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MonasteryAreaWa).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MonasteryName).HasMaxLength(250);
            entity.Property(e => e.MonasteryPhoneNo)
                .HasMaxLength(50)
                .HasColumnName("MonasteryPhoneNO");
            entity.Property(e => e.MonasteryTypeName).HasMaxLength(50);
            entity.Property(e => e.MooMonatery)
                .HasMaxLength(2)
                .HasColumnName("Moo_Monatery");
            entity.Property(e => e.Notation).HasMaxLength(500);
            entity.Property(e => e.PostCodeMonatery)
                .HasMaxLength(5)
                .HasColumnName("PostCode_Monatery");
            entity.Property(e => e.Preceptor).HasMaxLength(250);
            entity.Property(e => e.PreceptorTemple).HasMaxLength(250);
            entity.Property(e => e.ProvinceMonatery)
                .HasMaxLength(100)
                .HasColumnName("Province_Monatery");
            entity.Property(e => e.RecordStatus).HasMaxLength(10);
            entity.Property(e => e.RoadMonatery)
                .HasMaxLength(50)
                .HasColumnName("Road_Monatery");
            entity.Property(e => e.SubDistrictMonatery)
                .HasMaxLength(100)
                .HasColumnName("SubDistrict_Monatery");
            entity.Property(e => e.VillageMonatery)
                .HasMaxLength(50)
                .HasColumnName("Village_Monatery");
        });

        modelBuilder.Entity<TBranchRegister>(entity =>
        {
            entity.ToTable("T_Branch_Register");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AbbotEmail)
                .HasMaxLength(50)
                .HasColumnName("AbbotEMail");
            entity.Property(e => e.AbbotLineId)
                .HasMaxLength(50)
                .HasColumnName("AbbotLineID");
            entity.Property(e => e.AbbotName).HasMaxLength(250);
            entity.Property(e => e.AbbotPhoneNo)
                .HasMaxLength(50)
                .HasColumnName("AbbotPhoneNO");
            entity.Property(e => e.AbbotSignDate).HasColumnType("datetime");
            entity.Property(e => e.AbbotTemple).HasMaxLength(250);
            entity.Property(e => e.AbbotTitle).HasMaxLength(50);
            entity.Property(e => e.AddressTextAbbot)
                .HasMaxLength(500)
                .HasColumnName("AddressText_Abbot");
            entity.Property(e => e.AddressTextCertifier)
                .HasMaxLength(500)
                .HasColumnName("AddressText_Certifier");
            entity.Property(e => e.AddressTextMonatery)
                .HasMaxLength(500)
                .HasColumnName("AddressText_Monatery");
            entity.Property(e => e.AddressTextPreceptor)
                .HasMaxLength(500)
                .HasColumnName("AddressText_Preceptor");
            entity.Property(e => e.CertifierBranch).HasMaxLength(50);
            entity.Property(e => e.CertifierEmail)
                .HasMaxLength(50)
                .HasColumnName("CertifierEMail");
            entity.Property(e => e.CertifierLineId)
                .HasMaxLength(50)
                .HasColumnName("CertifierLineID");
            entity.Property(e => e.CertifierName).HasMaxLength(250);
            entity.Property(e => e.CertifierPhoneNo)
                .HasMaxLength(50)
                .HasColumnName("CertifierPhoneNO");
            entity.Property(e => e.CertifierSignDate).HasColumnType("datetime");
            entity.Property(e => e.CertifierTemple).HasMaxLength(250);
            entity.Property(e => e.CertifierTitle).HasMaxLength(50);
            entity.Property(e => e.CountryAbbot)
                .HasMaxLength(100)
                .HasColumnName("Country_Abbot");
            entity.Property(e => e.CountryCertifier)
                .HasMaxLength(100)
                .HasColumnName("Country_Certifier");
            entity.Property(e => e.CountryMonatery)
                .HasMaxLength(100)
                .HasColumnName("Country_Monatery");
            entity.Property(e => e.CountryPreceptor)
                .HasMaxLength(100)
                .HasColumnName("Country_Preceptor");
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfAcceptPosition).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DateOfFounding).HasColumnType("datetime");
            entity.Property(e => e.DateOfOrdination).HasColumnType("datetime");
            entity.Property(e => e.DepositaryName).HasMaxLength(50);
            entity.Property(e => e.DepositaryPhoneNo)
                .HasMaxLength(50)
                .HasColumnName("DepositaryPhoneNO");
            entity.Property(e => e.DistrictAbbot)
                .HasMaxLength(100)
                .HasColumnName("District_Abbot");
            entity.Property(e => e.DistrictCertifier)
                .HasMaxLength(100)
                .HasColumnName("District_Certifier");
            entity.Property(e => e.DistrictMonatery)
                .HasMaxLength(100)
                .HasColumnName("District_Monatery");
            entity.Property(e => e.DistrictPreceptor)
                .HasMaxLength(100)
                .HasColumnName("District_Preceptor");
            entity.Property(e => e.EcclesiasticalTitle).HasMaxLength(50);
            entity.Property(e => e.HouseNoAbbot)
                .HasMaxLength(50)
                .HasColumnName("HouseNo_Abbot");
            entity.Property(e => e.HouseNoCertifier)
                .HasMaxLength(50)
                .HasColumnName("HouseNo_Certifier");
            entity.Property(e => e.HouseNoMonatery)
                .HasMaxLength(50)
                .HasColumnName("HouseNo_Monatery");
            entity.Property(e => e.HouseNoPreceptor)
                .HasMaxLength(50)
                .HasColumnName("HouseNo_Preceptor");
            entity.Property(e => e.LandRightsDocuments).HasMaxLength(50);
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.ModifiedByName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.MonasteryAreaNgan).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MonasteryAreaRai).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MonasteryAreaWa).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MonasteryName).HasMaxLength(250);
            entity.Property(e => e.MonasteryPhoneNo)
                .HasMaxLength(50)
                .HasColumnName("MonasteryPhoneNO");
            entity.Property(e => e.MooAbbot)
                .HasMaxLength(2)
                .HasColumnName("Moo_Abbot");
            entity.Property(e => e.MooCertifier)
                .HasMaxLength(2)
                .HasColumnName("Moo_Certifier");
            entity.Property(e => e.MooMonatery)
                .HasMaxLength(2)
                .HasColumnName("Moo_Monatery");
            entity.Property(e => e.MooPreceptor)
                .HasMaxLength(2)
                .HasColumnName("Moo_Preceptor");
            entity.Property(e => e.Notation).HasMaxLength(500);
            entity.Property(e => e.PostCodeAbbot)
                .HasMaxLength(5)
                .HasColumnName("PostCode_Abbot");
            entity.Property(e => e.PostCodeCertifier)
                .HasMaxLength(5)
                .HasColumnName("PostCode_Certifier");
            entity.Property(e => e.PostCodeMonatery)
                .HasMaxLength(5)
                .HasColumnName("PostCode_Monatery");
            entity.Property(e => e.PostCodePreceptor)
                .HasMaxLength(5)
                .HasColumnName("PostCode_Preceptor");
            entity.Property(e => e.Preceptor).HasMaxLength(250);
            entity.Property(e => e.PreceptorTemple).HasMaxLength(250);
            entity.Property(e => e.ProvinceAbbot)
                .HasMaxLength(100)
                .HasColumnName("Province_Abbot");
            entity.Property(e => e.ProvinceCertifier)
                .HasMaxLength(100)
                .HasColumnName("Province_Certifier");
            entity.Property(e => e.ProvinceMonatery)
                .HasMaxLength(100)
                .HasColumnName("Province_Monatery");
            entity.Property(e => e.ProvincePreceptor)
                .HasMaxLength(100)
                .HasColumnName("Province_Preceptor");
            entity.Property(e => e.RecordStatus).HasMaxLength(10);
            entity.Property(e => e.RoadAbbot)
                .HasMaxLength(50)
                .HasColumnName("Road_Abbot");
            entity.Property(e => e.RoadCertifier)
                .HasMaxLength(50)
                .HasColumnName("Road_Certifier");
            entity.Property(e => e.RoadMonatery)
                .HasMaxLength(50)
                .HasColumnName("Road_Monatery");
            entity.Property(e => e.RoadPreceptor)
                .HasMaxLength(50)
                .HasColumnName("Road_Preceptor");
            entity.Property(e => e.SubDistrictAbbot)
                .HasMaxLength(100)
                .HasColumnName("SubDistrict_Abbot");
            entity.Property(e => e.SubDistrictCertifier)
                .HasMaxLength(100)
                .HasColumnName("SubDistrict_Certifier");
            entity.Property(e => e.SubDistrictMonatery)
                .HasMaxLength(100)
                .HasColumnName("SubDistrict_Monatery");
            entity.Property(e => e.SubDistrictPreceptor)
                .HasMaxLength(100)
                .HasColumnName("SubDistrict_Preceptor");
            entity.Property(e => e.VillageAbbot)
                .HasMaxLength(50)
                .HasColumnName("Village_Abbot");
            entity.Property(e => e.VillageCertifier)
                .HasMaxLength(50)
                .HasColumnName("Village_Certifier");
            entity.Property(e => e.VillageMonatery)
                .HasMaxLength(50)
                .HasColumnName("Village_Monatery");
            entity.Property(e => e.VillagePreceptor)
                .HasMaxLength(50)
                .HasColumnName("Village_Preceptor");
        });

        modelBuilder.Entity<TMonk>(entity =>
        {
            entity.ToTable("T_Monk");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AcademicStanding).HasMaxLength(100);
            entity.Property(e => e.AcademicStandingNo).HasMaxLength(100);
            entity.Property(e => e.AddressText).HasMaxLength(500);
            entity.Property(e => e.BelongingTemple).HasMaxLength(100);
            entity.Property(e => e.BloodType).HasMaxLength(50);
            entity.Property(e => e.CongenitalDisease).HasMaxLength(100);
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedByName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.DrugAllergy).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMail");
            entity.Property(e => e.EmergencyNumber).HasMaxLength(50);
            entity.Property(e => e.FaceBook).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(250);
            entity.Property(e => e.GraduateOfTheology).HasMaxLength(100);
            entity.Property(e => e.GraduateOfTheologyNo).HasMaxLength(100);
            entity.Property(e => e.LineId)
                .HasMaxLength(100)
                .HasColumnName("LineID");
            entity.Property(e => e.ModifiedByName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Monastery).HasMaxLength(100);
            entity.Property(e => e.MonkRank).HasMaxLength(100);
            entity.Property(e => e.Moo).HasMaxLength(2);
            entity.Property(e => e.NickName).HasMaxLength(50);
            entity.Property(e => e.Notation).HasMaxLength(500);
            entity.Property(e => e.OrdinationDate).HasColumnType("datetime");
            entity.Property(e => e.PassportNo).HasMaxLength(50);
            entity.Property(e => e.Pid)
                .HasMaxLength(15)
                .HasColumnName("PID");
            entity.Property(e => e.Preceptor).HasMaxLength(250);
            entity.Property(e => e.Province).HasMaxLength(100);
            entity.Property(e => e.RecordStatus).HasMaxLength(10);
            entity.Property(e => e.Road).HasMaxLength(50);
            entity.Property(e => e.SubDistrict).HasMaxLength(100);
            entity.Property(e => e.SurName).HasMaxLength(100);
            entity.Property(e => e.Theologian).HasMaxLength(100);
            entity.Property(e => e.TheologianNo).HasMaxLength(100);
            entity.Property(e => e.TitleConferredByTheKing).HasMaxLength(100);
            entity.Property(e => e.Village).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(5);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
