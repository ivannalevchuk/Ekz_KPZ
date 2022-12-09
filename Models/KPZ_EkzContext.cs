using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EKZ_KPZ.Models
{
    public partial class KPZ_EkzContext : DbContext
    {
        public KPZ_EkzContext()
        {
        }

        public KPZ_EkzContext(DbContextOptions<KPZ_EkzContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Playlists> Playlists { get; set; }
        public virtual DbSet<Songs> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.; Database=KPZ_Ekz ; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlists>(entity =>
            {
                entity.HasKey(e => e.PlaylistId);

                entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");

                entity.Property(e => e.PlaylistName).HasColumnName("playlist_name");
            });

            modelBuilder.Entity<Songs>(entity =>
            {
                entity.HasKey(e => e.SongId);

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");

                entity.Property(e => e.SongName).HasColumnName("song_name");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.PlaylistId)
                    .HasConstraintName("FK_Songs_Playlists");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
