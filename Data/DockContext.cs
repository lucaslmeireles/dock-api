using Microsoft.EntityFrameworkCore;

public class DockContext : DbContext
{
    public DbSet<Dock> Dock { get; set; }

    public DbSet<Truck> Truck { get; set; }
    public DbSet<Cargo> Cargo { get; set; }

    //TODO Remover essa referencia
    public DbSet<TruckOnDock> truckOnDocks { get; set; }

    public string connectionString = Environment.GetEnvironmentVariable("password");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"Host=ep-snowy-pond-ac7no5su-pooler.sa-east-1.aws.neon.tech;Database=dock;Username=neondb_owner;Password={connectionString};SSL Mode=Require;Channel Binding=Require");
        base.OnConfiguring(optionsBuilder);
    }
}