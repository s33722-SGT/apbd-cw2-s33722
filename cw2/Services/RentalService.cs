using cw2.Models;
using cw2.Repositories;

namespace cw2.Services;
using System;
using System.Collections.Generic;
public class RentalService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly List<Rental> _rentals = new List<Rental>();
    
    public RentalService(IEquipmentRepository equipmentRepo, IUserRepository userRepo)
        {
        _equipmentRepository = equipmentRepo;
        _userRepository = userRepo;
        }

    public Rental RentEquipment(Guid equipmentId, Guid userId, int durationDays = 7)
    {
        var equipment = _equipmentRepository.GetById(equipmentId);
        var user = _userRepository.GetById(userId);

        if (equipment == null || user == null)
        {
            throw new Exception("User or equipment not found");
        }

        if (equipment.IsDameged)
        {
            throw new Exception("Equipment is damaged");
        }
        
        bool isAlreadyRented = false;
        foreach (var rental in _rentals)
        {
            if (rental.RentedEquipment.Id == equipmentId && rental.ReturnDate == null)
            {
                isAlreadyRented = true;
                break;
            }
        }

        if (isAlreadyRented)
        {
            throw new Exception($"Equipment is not available, blocked: {user} - {equipment}");
        }

        int activeRentalsCount = 0;
        foreach (var rental in _rentals)
        {
            if (rental.RentedBy.Id == userId && rental.ReturnDate == null)
            {
                activeRentalsCount++;
            }
        }

        if (activeRentalsCount >= user.MaxRentals)
        {
            throw new Exception($"User has already reached the rental limit ({user.MaxRentals})");
        }
        var newRental = new Rental(equipment, user, durationDays);
        _rentals.Add(newRental);
        return newRental;
    }

    public void ReturnEquipment(Guid equipmentId)
    {
        Rental activeRental = null;
        foreach (var rental in _rentals)
        {
            if (rental.RentedEquipment.Id == equipmentId && rental.ReturnDate == null)
            {
                activeRental = rental;
                break;
            }
        }

        if (activeRental == null)
        {
            throw new Exception("Did not found active rental for this equipment");
        }
        activeRental.MarkAsReturned();
    }

    public bool IsEquipmentAvailable(Guid equipmentId)
    {
        var equipment = _equipmentRepository.GetById(equipmentId);
        if (equipment != null && equipment.IsDameged)
            return false;

        foreach (var rental in _rentals)
        {
            if (rental.RentedEquipment.Id == equipmentId && rental.ReturnDate == null)
            {
                return false;
            }   
        }
        return true;
    }

    public List<Rental> GetActiveRentalsForUser(Guid userId)
    {
        List<Rental> activeRentals = new List<Rental>();
        foreach (var rental in _rentals)
        {
            if (rental.RentedBy.Id == userId && rental.ReturnDate == null)
            {
                activeRentals.Add(rental);
            }
        }
        return activeRentals;
    }
    public List<Rental> GetOverdueRentals()
    {
        List<Rental> overdueRentals = new List<Rental>();
        foreach (var rental in _rentals)
        {
            if (rental.ReturnDate == null && DateTime.Now > rental.DueDate)
            {
                overdueRentals.Add(rental);
            }
        }
        return overdueRentals;
    }
}