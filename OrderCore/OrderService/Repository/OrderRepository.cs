using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repository;

public class OrderRepository : IRepository<Order>
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Order GetById(int id)
    {
        return _dbContext.Orders.FirstOrDefault(o => o.Id == id);
    }

    public List<Order> GetAll()
    {
        return _dbContext.Orders.ToList();
    }

    public Order Create(Order entity)
    {
        _dbContext.Orders.Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public Order Update(Order entity)
    {
        var order = GetById(entity.Id);
        if (order == null) return null;

        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public bool Delete(int id)
    {
        var order = GetById(id);
        if (order == null) return false;

        _dbContext.Remove(order);
        _dbContext.SaveChanges();
        return true;
    }
}
