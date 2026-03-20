namespace cw2.Models;

public class Laptop : Equipment
{
    public int RamGb { get; private set; }
    public string Processor { get; private set; }

    public Laptop(string name, int ramGb, string processor) : base(name)
    {
        RamGb = ramGb;
        Processor = processor;
    }
}