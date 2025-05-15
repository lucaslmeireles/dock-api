using Microsoft.EntityFrameworkCore;

public class DockContext : DbContext
{
    public DbSet<Dock> Dock { get; set; }

    public DbSet<Truck> Truck { get; set; }
    public DbSet<Cargo> Cargo { get; set; }

    //TODO Remover essa referencia
    public DbSet<TruckOnDock> truckOnDocks { get; set; }

    public string connectionString = Environment.GetEnvironmentVariable("DB_URL") ?? "postgres://yourusername:yourpassword@localhost:5432/dock";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        base.OnConfiguring(optionsBuilder);
    }
}