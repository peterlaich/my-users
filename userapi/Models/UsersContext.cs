using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace userapi.Models
{
    public partial class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=users;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Clients", "Domain");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.Mgr)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.MgrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientsManagers");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Clients)
                    .HasForeignKey<Clients>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientsUsers");
            });

            modelBuilder.Entity<Managers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Managers", "Domain");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Managers)
                    .HasForeignKey<Managers>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerUsers");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Users", "Domain");

                entity.Property(e => e.Alias).HasMaxLength(1000);

                entity.Property(e => e.Email).HasMaxLength(1000);

                entity.Property(e => e.FirstName).HasMaxLength(1000);

                entity.Property(e => e.LastName).HasMaxLength(1000);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(200);
            });
        }
    }
}
