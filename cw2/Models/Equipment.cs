using System;
namespace cw2.Models;
public abstract class Equipment
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public bool IsDameged { get; private set; } = false;
    
    protected Equipment(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public void MarkAsDameged()
    {
        IsDameged = true;
    }
    override public string ToString()
    {
        return $"{GetType().Name} {Name}";
    }
}