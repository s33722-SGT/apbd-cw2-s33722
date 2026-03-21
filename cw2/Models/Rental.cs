namespace cw2.Models;
using System;
public class Rental
{
    public Guid Id { get; private set; }
    public Equipment RentedEquipment { get; private set; }
    public User RentedBy { get; private set; }
    public DateTime RentalDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    
    public Rental(Equipment equipment, User user, int rentDurationDays = 7)
    {
        Id = Guid.NewGuid();
        RentedEquipment = equipment;
        RentedBy = user;
        RentalDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(rentDurationDays);
    }

    public void MarkAsReturned()
    {
        ReturnDate = DateTime.Now;
    }
}