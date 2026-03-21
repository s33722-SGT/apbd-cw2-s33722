using cw2.Models;

namespace cw2.Repositories;
using  System;
using System.Collections.Generic;
public interface IEquipmentRepository
{
    void Add(Equipment equipment);
    Equipment GetById(Guid id);
    IEnumerable<Equipment> GetAll();
}