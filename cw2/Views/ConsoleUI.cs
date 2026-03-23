using cw2.Models;
using cw2.Repositories;
using cw2.Services;

namespace cw2.Views;

public class ConsoleUI
{
    private readonly IEquipmentRepository _equipmentRepo;
    private readonly RentalService _rentalService;

    public ConsoleUI(IEquipmentRepository equipmentRepo, RentalService rentalService)
    {
        _equipmentRepo = equipmentRepo;
        _rentalService = rentalService;
    }
    
    public void PrintAllEquipments()
    {
        Console.WriteLine("\nAll equipments:");
        foreach (var equipment in _equipmentRepo.GetAll())
        {
            bool isAvailable = _rentalService.IsEquipmentAvailable(equipment.Id);
            string statusText = isAvailable ? "Available" : "Unavailable";
            Console.WriteLine($"- {equipment} | {statusText}");
        }
    }
    public void PrintAvailableEquipments()
    {
        Console.WriteLine("\nAvailable equipments:");
        foreach (var equipment in _equipmentRepo.GetAll())
        {
            bool isAvailable = _rentalService.IsEquipmentAvailable(equipment.Id);
            if (isAvailable)
                Console.WriteLine($"- {equipment}");
        }
    }

    public void PrintActiveRentalsForUser(User user)
    {
        Console.WriteLine($"\nActive rentals for user: {user}");
        var rentals = _rentalService.GetActiveRentalsForUser(user.Id);
        if (rentals.Count == 0)
        {
            Console.WriteLine("No active rentals for this user");
            return;
        }

        foreach (var rental in rentals)
        {
            Console.WriteLine($"- {rental}");
        }
    }

    public void PrintOverdueRentals()
    {
        Console.WriteLine("\nOverdue rentals:");
        var overdue = _rentalService.GetOverdueRentals();

        if (overdue.Count == 0)
        {
            Console.WriteLine("No overdue rentals");
            return;
        }
        foreach (var rental in overdue)
            Console.WriteLine($"- {rental}");
    }

    public void PrintSystemReport()
    {
        Console.WriteLine("\nRental system report:");
        
        int totalEquipmentCount = _equipmentRepo.GetAll().Count();
        int activeRentalsCount = 0;
        int demegedEquipmentCount = 0;

        foreach (var equipment in _equipmentRepo.GetAll())
        {
            if (equipment.IsDameged)
            {
                demegedEquipmentCount++;
            }
            else if (!_rentalService.IsEquipmentAvailable(equipment.Id))
            {
                activeRentalsCount++;
            }
        }

        int availableCount = totalEquipmentCount - demegedEquipmentCount - activeRentalsCount;
        int overdueEquipmentCount = _rentalService.GetOverdueRentals().Count;
        
        Console.WriteLine($"Total equipments in system: {totalEquipmentCount}");
        Console.WriteLine($"Currently rented equipments: {activeRentalsCount}");
        Console.WriteLine($"Damaged equipments: {demegedEquipmentCount}");
        Console.WriteLine($"Available equipments: {availableCount}");
        Console.WriteLine($"Overdue equipments: {overdueEquipmentCount}");
        
    }
}