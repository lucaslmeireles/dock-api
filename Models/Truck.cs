public class Truck
{
    public Truck(string driverName, string carrierName)
    {
        Id = Guid.NewGuid();
        DriverName = driverName;
        CarrierName = carrierName;
    }
    public Guid Id { get; init; }
    public string DriverName { get; private set; }
    public string CarrierName { get; private set; }

    public ICollection<Cargo> Cargo { get; set; }

    public void ChangeDriverName(string driverName)
    {
        DriverName = driverName;
    }
    public void ChangeCarrierName(string carrierName)
    {
        CarrierName = carrierName;
    }
}