using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RealEstateManagement__TranThaiThinh.Repositories.Models;

public partial class FA25RealEstateDBContext : DbContext
{
    public FA25RealEstateDBContext()
    {
    }

    public FA25RealEstateDBContext(DbContextOptions<FA25RealEstateDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Broker> Brokers { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-0S0P0DLI\\SQL;Initial Catalog=FA25RealEstateDB;Persist Security Info=True;User ID=sa;Password=12345");

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        string connectionstring = config.GetConnectionString(connectionStringName);
        return connectionstring;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Broker>(entity =>
        {
            entity.HasKey(e => e.BrokerId).HasName("PK__Broker__5D1D9A306CDA0FF1");

            entity.ToTable("Broker");

            entity.Property(e => e.BrokerId).HasColumnName("BrokerID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__C90D34099AB43109");

            entity.ToTable("Contract");

            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.BrokerId).HasColumnName("BrokerID");
            entity.Property(e => e.ContractTitle)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.PropertyType)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Broker).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.BrokerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contract__Broker__2A4B4B5E");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__SystemUs__1788CCAC7F90F4FB");

            entity.ToTable("SystemUser");

            entity.HasIndex(e => e.Username, "UQ__SystemUs__536C85E446D2EC11").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserPassword)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}