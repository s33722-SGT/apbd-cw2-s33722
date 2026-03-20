namespace cw2.Models;
using System;
public abstract class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    
    public abstract int MaxRentals { get; }
    
    protected User(string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }
}