public class Truck
{
    public Truck(String driverName, String carrierName)
    {
        Id = Guid.NewGuid();
        DriverName = driverName;
        CarrierName = carrierName;
    }
    public Guid Id { get; init; }
    public String DriverName { get; private set; }
    public String CarrierName { get; private set; }

    public ICollection<Cargo> Cargo { get; set; }
}