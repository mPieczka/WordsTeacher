using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WordsTeacher.Data.Entities;

namespace WordsTeacher.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Test> Tests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PhraseTestMapping>().HasKey(a => new { a.TestId, a.PhraseId });
            builder.Entity<Test>().HasMany(a => a.Phrases).WithOne(a => a.Test).HasForeignKey(a => a.TestId);
            builder.Entity<Phrase>().HasMany(a => a.Tests).WithOne(a => a.Phrase).HasForeignKey(a => a.PhraseId);
            base.OnModelCreating(builder);
        }
    }
}