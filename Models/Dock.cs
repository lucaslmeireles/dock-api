public class Dock
{
    public Dock(String name, int slots)
    {
        Id = Guid.NewGuid();
        Name = name;
        Slots = slots;
    }
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public int Slots { get; private set; }
}