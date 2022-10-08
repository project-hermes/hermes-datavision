using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hermes_datavision.DAL
{
    // dotnet ef dbcontext scaffold "CONNECTION STRING" Microsoft.EntityFrameworkCore.SqlServer -o DAL -c "HermesDbContext"
    public partial class HermesDbContext : DbContext
    {
        public HermesDbContext()
        {
        }

        public HermesDbContext(DbContextOptions<HermesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Firmware> Firmwares { get; set; } = null!;
        public virtual DbSet<Remora> Remoras { get; set; } = null!;
        public virtual DbSet<RemoraRecord> RemoraRecords { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firmware>(entity =>
            {
                entity.ToTable("Firmware");
            });

            modelBuilder.Entity<Remora>(entity =>
            {
                entity.ToTable("Remora");

                entity.Property(e => e.DeviceId).HasColumnName("deviceId");

                entity.Property(e => e.DiveId).HasColumnName("diveId");

                entity.Property(e => e.EndLat).HasColumnName("endLat");

                entity.Property(e => e.EndLng).HasColumnName("endLng");

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.Freq).HasColumnName("freq");

                entity.Property(e => e.Mode).HasColumnName("mode");

                entity.Property(e => e.StartLat).HasColumnName("startLat");

                entity.Property(e => e.StartLng).HasColumnName("startLng");

                entity.Property(e => e.StartTime).HasColumnName("startTime");
            });

            modelBuilder.Entity<RemoraRecord>(entity =>
            {
                entity.ToTable("RemoraRecord");

                entity.HasIndex(e => e.RemoraId, "IX_RemoraRecord_RemoraId");

                entity.Property(e => e.Degrees).HasColumnName("degrees");

                entity.Property(e => e.Depth).HasColumnName("depth");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.TimestampDouble).HasColumnName("timestampDouble");

                entity.HasOne(d => d.Remora)
                    .WithMany(p => p.RemoraRecords)
                    .HasForeignKey(d => d.RemoraId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
