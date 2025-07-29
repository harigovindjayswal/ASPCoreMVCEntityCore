using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreMVCEntityCore.models;

public partial class MyContext : DbContext
{
    public MyContext()
    {
    }

    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DistrictMaster> DistrictMasters { get; set; }

    public virtual DbSet<StateMaster> StateMasters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DistrictMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DistrictMaster");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StateId).HasColumnName("stateId");
        });

        modelBuilder.Entity<StateMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StateMaster");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("district");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.FilePath)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("filePath");
            entity.Property(e => e.Gender)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Hoby)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hoby");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhotoPath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("photoPath");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
