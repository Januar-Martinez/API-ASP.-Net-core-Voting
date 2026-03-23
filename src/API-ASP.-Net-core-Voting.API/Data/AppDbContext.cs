using Microsoft.EntityFrameworkCore;
using API_ASP._Net_core_Voting.API.Models;

namespace API_ASP._Net_core_Voting.API.Data
{

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Voter> Voters { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Voter>(entity =>
            {
                entity.HasIndex(v => v.Email)
                      .IsUnique();

                entity.Property(v => v.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(v => v.Email)
                      .IsRequired()
                      .HasMaxLength(150);
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasIndex(c => c.Name)
                      .IsUnique();

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Party)
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasIndex(v => v.VoterId)
                      .IsUnique();

                entity.HasOne(v => v.Voter)
                      .WithOne(voter => voter.Vote)
                      .HasForeignKey<Vote>(v => v.VoterId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(v => v.Candidate)
                      .WithMany(c => c.VotesReceived)
                      .HasForeignKey(v => v.CandidateId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}