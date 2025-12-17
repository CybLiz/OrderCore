namespace OrderService.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll();
        T Create(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
