using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CashHandlerAPI.Models
{
    public partial class CashHandlerDBContext :IdentityDbContext
    {
        public CashHandlerDBContext()
        {
        }

        public CashHandlerDBContext(DbContextOptions<CashHandlerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CashBalance> CashBalances { get; set; }
        public virtual DbSet<Register> Registers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CashBalance>(entity =>
            {
                entity.ToTable("CashBalance");

                entity.Property(e => e.CashBalanceId).HasColumnName("CashBalanceID");

                entity.Property(e => e.Currency).HasMaxLength(50);
            });

            modelBuilder.Entity<Register>(entity =>
            {
                entity.ToTable("Register");

                entity.Property(e => e.RegisterId).HasColumnName("RegisterID");

                entity.Property(e => e.CashAmountId).HasColumnName("CashAmountID");

                entity.Property(e => e.RegisterLocation).HasMaxLength(500);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Denominations).HasMaxLength(500);

                entity.Property(e => e.TransDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Transactions_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.LastSignIn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RegisterId).HasColumnName("RegisterID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RegisterId)
                    .HasConstraintName("FK_Users_Register");
            });

           
        }

     
    }
}
