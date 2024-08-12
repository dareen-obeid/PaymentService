using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentTransaction>()
                .HasOne(pt => pt.PaymentStatus)
                .WithMany(ps => ps.PaymentTransactions)
                .HasForeignKey(pt => pt.PaymentStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentTransaction>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<PaymentStatus>()
              .Property(ps => ps.StatusId)
              .ValueGeneratedOnAdd();

        }
    }

}