using Lab3BD.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab3BD
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options)
            : base(options)
        { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().ToTable("Artist", "dbo");
            modelBuilder.Entity<Album>().ToTable("Album", "dbo");
            modelBuilder.Entity<Track>().ToTable("Track", "dbo");

        }
    }
}
