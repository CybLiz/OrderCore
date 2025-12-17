using UserService.Models;
using UserService.Data;

namespace UserService.Repository;

public class UserRepository : IRepository<User>
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetById(int id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public List<User> GetAll()
    {
        return _dbContext.Users.ToList();
    }

    public User Create(User entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public User Update(User entity)
    {
        var user = GetById(entity.Id);
        if (user == null) return null;

        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public bool Delete(int id)
    {
        var user = GetById(id);
        if (user == null) return false;

        _dbContext.Remove(user);
        _dbContext.SaveChanges();
        return true;
    }
}
