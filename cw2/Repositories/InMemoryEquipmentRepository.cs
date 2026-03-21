using cw2.Models;

namespace cw2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _equipment = new List<Equipment>();

    public void Add(Equipment equipment)
    {
        _equipment.Add(equipment);
    }

    public Equipment GetById(Guid id)
    {
        foreach (var equipment in _equipment)
        {
            if (equipment.Id == id)
                return equipment;
        }
        return null;
    }

    public IEnumerable<Equipment> GetAll()
    {
        return _equipment;
    }
}