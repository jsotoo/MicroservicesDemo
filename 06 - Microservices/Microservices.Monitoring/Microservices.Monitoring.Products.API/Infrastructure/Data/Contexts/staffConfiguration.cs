﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microservices.Monitoring.Products.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microservices.Monitoring.Products.API.Infrastructure.Data.Contexts
{
    public class staffConfiguration : IEntityTypeConfiguration<staff>
    {
        public void Configure(EntityTypeBuilder<staff> entity)
        {
            entity.ToTable("Staff", "Sales");

            entity.HasIndex(e => e.Email)
                .HasName("UQ__Staff__A9D10534C13DBB09")
                .IsUnique();

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Manager)
                .WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Staff__ManagerId__398D8EEE");

            entity.HasOne(d => d.Store)
                .WithMany(p => p.staff)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Staff__StoreId__3A81B327");
        }
    }
}