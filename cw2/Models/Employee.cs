namespace cw2.Models;

public class Employee : User
{
    public override int MaxRentals => 5;
    
    public Employee(string firstName, string lastName) : base(firstName, lastName)
    {}
}