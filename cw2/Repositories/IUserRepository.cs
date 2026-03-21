using cw2.Models;

namespace cw2.Repositories;
using System;
using System.Collections.Generic;
public interface IUserRepository
{
    void Add(User user);
    User GetById(Guid id);
    IEnumerable<User> GetAll();
}