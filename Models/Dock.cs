public class Dock
{
    public Dock(string name, int slots)
    {
        Id = Guid.NewGuid();
        Name = name;
        Slots = slots;
    }
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public int Slots { get; private set; }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeSlots(int slots)
    {
        Slots = slots;
    }

    public void SetInactive()
    {
        Name = "DESATIVADO";
    }
}