public class Cargo
{
    public Cargo(string productName, string receiptNumber, string supplier)
    {
        Id = Guid.NewGuid();
        ProductName = productName;
        ReceiptNumber = receiptNumber;
        Supplier = supplier;
    }
    public Guid Id { get; init; }
    public string ProductName { get; private set; }
    public string ReceiptNumber { get; private set; }
    public string Supplier { get; private set; }

    public Guid TruckId { get; set; }

    public int quantity { get; private set; }

    public bool IsLoad {  get; private set; }
    public void LoadOrUnload(Guid truckId)
    {
        TruckId = truckId;
    }
}