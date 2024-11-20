using Jeremy.ObjectCollectionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Jeremy.ObjectCollectionSystem.Services;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBasicConfig> TbBasicConfigs { get; set; } = null!;
    public virtual DbSet<TbJobConfig> TbJobConfigs { get; set; } = null!;
    public virtual DbSet<TbJobLog> TbJobLogs { get; set; } = null!;
    public virtual DbSet<TbKafkaConfig> TbKafkaConfigs { get; set; } = null!;
    public virtual DbSet<TbMinioConfig> TbMinioConfigs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite($@"Data Source={System.Threading.Thread.GetDomain().BaseDirectory}db.db3;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbBasicConfig>(entity =>
        {
            entity.ToTable("tb_basic_config");

            entity.Property(e => e.CreateTime).HasColumnType("DATETIME");

            entity.Property(e => e.UpdateTime).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<TbJobConfig>(entity =>
        {
            entity.ToTable("tb_job_config");

            entity.Property(e => e.CreateTime).HasColumnType("DATETIME");

            entity.Property(e => e.IsDelete)
                .HasColumnType("BOOL")
                .HasDefaultValueSql("false");

            entity.Property(e => e.IsRunning)
                .HasColumnType("BOOL")
                .HasDefaultValueSql("false");

            entity.Property(e => e.ScanIntervalType).HasDefaultValueSql("0");

            entity.Property(e => e.ThreadCount).HasDefaultValueSql("1");

            entity.Property(e => e.UpdateTime).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<TbJobLog>(entity =>
        {
            entity.ToTable("tb_job_log");

            entity.Property(e => e.CreateTime).HasColumnType("DATETIME");

            entity.Property(e => e.IsKafka).HasColumnType("BOOL");

            entity.Property(e => e.IsKafkaTime).HasColumnType("DATETIME");

            entity.Property(e => e.IsMinIo)
                .HasColumnType("BOOL")
                .HasColumnName("IsMinIO");

            entity.Property(e => e.IsMinIotime)
                .HasColumnType("DATETIME")
                .HasColumnName("IsMinIOTime");

            entity.Property(e => e.MinIobucketName).HasColumnName("MinIOBucketName");

            entity.Property(e => e.MinIofileName).HasColumnName("MinIOFileName");

            entity.Property(e => e.MinIopath).HasColumnName("MinIOPath");

            entity.Property(e => e.MinIourl).HasColumnName("MinIOUrl");
        });

        modelBuilder.Entity<TbKafkaConfig>(entity =>
        {
            entity.ToTable("tb_kafka_config");

            entity.Property(e => e.CreateTime).HasColumnType("DATETIME");

            entity.Property(e => e.Topic).HasDefaultValueSql("'mdc_to_eap_image'");

            entity.Property(e => e.UpdateTime).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<TbMinioConfig>(entity =>
        {
            entity.ToTable("tb_minio_config");

            entity.Property(e => e.CreateTime).HasColumnType("DATETIME");

            entity.Property(e => e.Path).HasDefaultValueSql("'\\'");

            entity.Property(e => e.UpdateTime).HasColumnType("DATETIME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

