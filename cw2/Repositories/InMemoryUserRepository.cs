using cw2.Models;

namespace cw2.Repositories;
using System;
using System.Collections.Generic;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();

    public void Add(User user)
    {
        _users.Add(user);
    }
    
    public User GetById(Guid id)
    {
        foreach (var user in _users)
        {
            if (user.Id == id)
                return user;
        }
        return null;
    }
    
    public IEnumerable<User> GetAll()
    {
        return _users;
    }
}