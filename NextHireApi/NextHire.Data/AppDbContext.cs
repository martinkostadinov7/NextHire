using Microsoft.EntityFrameworkCore;
using NextHire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHire.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=DESKTOP-U242LRB\\SQLEXPRESS;Database=NextHireDb;Trusted_Connection=true;TrustServerCertificate=true;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Cv> Cvs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Application → User
            modelBuilder.Entity<Application>()
                .HasOne(a => a.User)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // само този FK може да има Cascade

            // Application → Offer
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Offer)
                .WithMany(o => o.Applications)
                .HasForeignKey(a => a.OfferId)
                .OnDelete(DeleteBehavior.Restrict); // няма cascade

            // Application → Cv
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Cv)
                .WithMany()
                .HasForeignKey(a => a.CvId)
                .OnDelete(DeleteBehavior.Restrict); // Cv остава, ако Application се изтрие

            // Offer → Company
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Company)
                .WithMany()
                .HasForeignKey(o => o.CompanyId)
                .OnDelete(DeleteBehavior.Restrict); // Company не три оферти

            // Cv → User
            modelBuilder.Entity<Cv>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); // ако User се изтрие, неговите CV-та също
        }


    }
}
