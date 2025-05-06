public class TruckOnDock
{
    public TruckOnDock(Guid truckId, Guid dockId, int slot)
    {
        TruckId = truckId;
        DockId = dockId;
        Slot = slot;

    }
    public Guid TruckId { get; set; }
    public Truck Truck { get; set; }
    public Guid DockId { get; set; }
    public Dock Dock { get; set; }
    public DateTime ArrivalTime { get; private set; } = DateTime.UtcNow;
    public DateTime DepartureTime { get; private set; }
    public Integer Slot { get; private set; }
}