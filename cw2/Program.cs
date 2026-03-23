using System;
using cw2.Models;
using cw2.Repositories;
using cw2.Services;

IEquipmentRepository equipmentRepo = new InMemoryEquipmentRepository();
IUserRepository userRepo = new InMemoryUserRepository();

RentalService rentalService = new RentalService(equipmentRepo, userRepo);
PenaltyCalculator penaltyCalculator = new PenaltyCalculator();

var lenovoThinkBook = new Laptop("Lenovo ThinkBook 14-Are G2", 16, "AMD Ryzen 7 4700U");
var logitechGPRO = new Mouse("Logitech G Pro X Superlight", 25600, true);
var Steelseries600 = new Mouse("Steelseries Rival 600", 12000, false);
var SonyA7IV = new Camera("Aony A7 IV", "Sony E-mount", true);

equipmentRepo.Add(lenovoThinkBook);
equipmentRepo.Add(logitechGPRO);
equipmentRepo.Add(Steelseries600);
equipmentRepo.Add(SonyA7IV);

var s33722 = new Student("Piotr", "Sztenke");
userRepo.Add(s33722);
var e123 = new Employee("Marek", "Kucharek");
userRepo.Add(e123);

Rental rental1 = null; 
Rental rental2 = null; 
Rental rental3 = null; 
Rental rental4 = null;
Rental rental5 = null;
Rental rental6 = null;


RunScenario($"1. {s33722} próbuje wypożyczyć: {lenovoThinkBook}", () => 
{
    rental1 = rentalService.RentEquipment(lenovoThinkBook.Id, s33722.Id, 100);
    Console.WriteLine("    wypożyczenie udane (na 100 dni)");
    rental1.SimulatePassedDays(105);
    Console.WriteLine("    Sztucznie cofnięto w czasie daty dla tego wypożyczenia o 105 dni");
    Console.WriteLine($"    kara dla wypożyczenia wynosi: {penaltyCalculator.CalculatePenalty(rental1)} zł");
    Console.WriteLine(rental1);
    
});

RunScenario($"2. {e123} próbuje wypożyczyć: {Steelseries600}", () => 
{
    rental2 = rentalService.RentEquipment(Steelseries600.Id, e123.Id);
    Console.WriteLine("    wypożyczenie udane (na domyślnie 7 dni)");
    rental2.SimulatePassedDays(1);
    Console.WriteLine("    Sztucznie cofnięto w czasie daty dla tego wypożyczenia o 1 dzień");
    Console.WriteLine($"    kara dla wypożyczenia wynosi: {penaltyCalculator.CalculatePenalty(rental2)} zł");
    Console.WriteLine(rental2);
});

RunScenario($"3. {s33722} próbuje wypożyczyć: {SonyA7IV}", () => 
{
    rental3 =  rentalService.RentEquipment(SonyA7IV.Id, s33722.Id);
    Console.WriteLine("    drugie wypożyczenie udane (na domyślnie 7 dni)");
    Console.WriteLine(rental3);
    
});

RunScenario($"4. {s33722} oddaje: {lenovoThinkBook.Name}", () => 
{
    rentalService.ReturnEquipment(rental1.RentedEquipment.Id); 
    Console.WriteLine("    zwrot udany");
    Console.WriteLine(rental1);
});

RunScenario($"5. {s33722.FirstName} PONOWNIE próbuje wypożyczyć: {SonyA7IV}", () => 
{
    rental4 = rentalService.RentEquipment(SonyA7IV.Id, s33722.Id);
    Console.WriteLine("-> SUKCES: Sprzęt wydany");
    Console.WriteLine(rental4);
});



void RunScenario(string description, Action actionToRun)
{
    Console.WriteLine($"\n{description}");
    try
    {
        actionToRun(); 
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red; 
        Console.WriteLine("    " + ex.Message); 
        Console.ResetColor();
    }
}

