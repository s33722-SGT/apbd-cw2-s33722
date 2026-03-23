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

    public bool WasReturnedOnTime
    {
        get
        {
            if (ReturnDate == null)
                return false;
            return  ReturnDate.Value <= DueDate;
        }
    }
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

    public void SimulatePassedDays(int days)
    {
        RentalDate = RentalDate.AddDays(-days);
        DueDate = DueDate.AddDays(-days);
    }

    public override string ToString()
    {
        string rentalDateStr = RentalDate.ToString("yyyy-MM-dd");
        string status;
        if (ReturnDate == null)
        {
            status = $"Active (until: {DueDate:yyyy-MM-dd})";
        }
        else
        {
            string onTime = WasReturnedOnTime ? "Yes" : "No";
            status = $"Returned {ReturnDate.Value:yyyy-MM-dd} (due time was: {onTime})";
        }
        
 
        return $"[Rental] {RentedEquipment.Name} -> {RentedBy.FirstName} {RentedBy.LastName} | Since: {rentalDateStr} | Status: {status}";
    }
}