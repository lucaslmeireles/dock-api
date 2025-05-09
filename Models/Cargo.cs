public class Cargo
{
    public Cargo(string productName, string receiptNumber, string supplier, int quantity, bool isLoad)
    {
        ProductName = productName;
        ReceiptNumber = receiptNumber;
        Supplier = supplier;
        Quantity = quantity;
        IsLoad = isLoad;
    }
    public Guid Id { get; init; }
    public string ProductName { get; private set; }
    public string ReceiptNumber { get; private set; }
    public string Supplier { get; private set; }

    public Guid TruckId { get; set; }
    public Truck Truck { get; set; }

    // Change to Quantity
    public int Quantity { get; private set; } = 0;

    public bool IsLoad { get; private set; } = false;
    public void LoadOrUnload(Guid truckId)
    {
        TruckId = truckId;
    }
}