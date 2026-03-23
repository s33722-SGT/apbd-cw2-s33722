using cw2.Models;
using cw2.Repositories;
using cw2.Services;
using cw2.Views;

IEquipmentRepository equipmentRepo = new InMemoryEquipmentRepository();
IUserRepository userRepo = new InMemoryUserRepository();
RentalService rentalService = new RentalService(equipmentRepo, userRepo);
PenaltyCalculator penaltyCalculator = new PenaltyCalculator();
ConsoleUI consoleUi = new ConsoleUI(equipmentRepo, rentalService);

var lenovoThinkBook = new Laptop("Lenovo ThinkBook 14-Are G2", 16, "AMD Ryzen 7 4700U");
var logitechGPRO = new Mouse("Logitech G Pro X Superlight", 25600, true);
var logitechGPRO2 = new Mouse("Logitech G Pro X Superlight 2", 44000, true);
var logitechGPRO3 = new Mouse("Logitech G Pro X Superlight 3", 44000, true);
var logitechGPRO4 = new Mouse("Logitech G Pro X Superlight 4", 44000, true);
var logitechGPRO5 = new Mouse("Logitech G Pro X Superlight 5", 44000, true);
var logitechGPRO6 = new Mouse("Logitech G Pro X Superlight 6", 44000, true);
var Steelseries600 = new Mouse("Steelseries Rival 600", 12000, false);
var SonyA7IV = new Camera("Aony A7 IV", "Sony E-mount", true);
var ZepsutyLaptop = new Laptop("HP zepsuty (dla testu)", 8, "Intel Core i5");
ZepsutyLaptop.MarkAsDameged();

equipmentRepo.Add(lenovoThinkBook);
equipmentRepo.Add(logitechGPRO);
equipmentRepo.Add(logitechGPRO2);
equipmentRepo.Add(logitechGPRO3);
equipmentRepo.Add(logitechGPRO4);
equipmentRepo.Add(logitechGPRO5);
equipmentRepo.Add(logitechGPRO6);
equipmentRepo.Add(Steelseries600);
equipmentRepo.Add(SonyA7IV);
equipmentRepo.Add(ZepsutyLaptop);

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
Rental rental7 = null;
Rental rental8 = null;
Rental rental9 = null;
Rental rental10 = null;

consoleUi.PrintAllEquipments();

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
    Console.WriteLine($"    kara dla wypożyczenia wynosi: {penaltyCalculator.CalculatePenalty(rental1)} zł");
    Console.WriteLine(rental1);
});

RunScenario($"5. {s33722} PONOWNIE próbuje wypożyczyć: {SonyA7IV}", () => 
{
    rental4 = rentalService.RentEquipment(SonyA7IV.Id, s33722.Id);
    Console.WriteLine("-> SUKCES: Sprzęt wydany");
    Console.WriteLine(rental4);
});

RunScenario($"6. {s33722} próbuje wypożyczyć zepsutego laptopa: {ZepsutyLaptop}", () => 
{
    rental5 = rentalService.RentEquipment(ZepsutyLaptop.Id, s33722.Id);
    Console.WriteLine("-> SUKCES: Sprzęt wydany");
    Console.WriteLine(rental5);
});

RunScenario($"6. {s33722} próbuje wypożyczyć za duzo sprzetu", () => 
{
    rental6 = rentalService.RentEquipment(logitechGPRO2.Id, s33722.Id);
    Console.WriteLine("-> SUKCES: Sprzęt wydany");
    consoleUi.PrintActiveRentalsForUser(s33722);
    Console.WriteLine("    Próba dalszego wypożyczania: ");
    rental7 = rentalService.RentEquipment(logitechGPRO3.Id, s33722.Id);
});

consoleUi.PrintAllEquipments();
consoleUi.PrintAvailableEquipments();
consoleUi.PrintActiveRentalsForUser(s33722);
rental2.SimulatePassedDays(999);
consoleUi.PrintActiveRentalsForUser(e123);
consoleUi.PrintOverdueRentals();
consoleUi.PrintSystemReport();

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

