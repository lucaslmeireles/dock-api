public record CargoRequest(string productName, string receiptNumber, string supplier, Guid truckId, int quantity, bool isLoad)
{
}