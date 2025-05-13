using Microsoft.EntityFrameworkCore;

public class DockContext : DbContext
{
    public DbSet<Dock> Dock { get; set; }

    public DbSet<Truck> Truck { get; set; }
    public DbSet<Cargo> Cargo { get; set; }

    //TODO Remover essa referencia
    public DbSet<TruckOnDock> truckOnDocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=dock.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}