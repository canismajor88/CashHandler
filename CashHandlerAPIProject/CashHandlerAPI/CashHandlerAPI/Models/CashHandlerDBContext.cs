using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CashHandlerAPI.Models
{
    public partial class CashHandlerDBContext : IdentityDbContext<User>
    {

        public CashHandlerDBContext(DbContextOptions<CashHandlerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> AspNetUsers { get; set; }
        public virtual DbSet<MoneyAmount> MoneyAmounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.MoneyAmountId, "IX_AspNetUsers_MoneyAmountID");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LastSignIn).HasColumnType("datetime");

                entity.Property(e => e.MoneyAmountId).HasColumnName("MoneyAmountID");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.MoneyAmount)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.MoneyAmountId);
            });


            modelBuilder.Entity<MoneyAmount>(entity =>
            {
                entity.ToTable("MoneyAmount");

                entity.Property(e => e.MoneyAmountId).HasColumnName("MoneyAmountID");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Transactions_UserID");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Denominations).HasMaxLength(500);

                entity.Property(e => e.TransDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Transactions_Users");
            });

          
        }

   
    }
}
