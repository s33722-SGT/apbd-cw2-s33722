using System;
namespace cw2.Models;
public abstract class Equipment
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsAvailable { get; private set; }
    
    protected Equipment(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsAvailable = true;
    }
    
    public void MarkAsAvailable()
    {
        IsAvailable = true;
    }
    public void MarkAsUnavailable()
    {
        IsAvailable = false;
    }

    override public string ToString()
    {
        return $"{GetType().Name} {Name}";
    }
}