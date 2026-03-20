namespace cw2.Models;

public class Mouse : Equipment
{
    public int Dpi { get; private set; }
    public bool IsWireless { get; private set; }

    public Mouse(string name, int dpi, bool isWireless) : base(name)
    {
        Dpi = dpi;
        IsWireless = isWireless;
    }
}