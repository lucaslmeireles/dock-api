public class Cargo
{
    public Cargo(String productName, String receiptNumber, String supplier)
    {
        Id = Guid.NewGuid();
        ProductName = productName;
        ReceiptNumber = receiptNumber;
        Supplier = supplier;
    }
    public Guid Id { get; init; }
    public String ProductName { get; private set; }
    public String ReceiptNumber { get; private set; }
    public String Supplier { get; private set; }

    public Guid TruckId { get; set; }
    public Truck Truck { get; set; }
}