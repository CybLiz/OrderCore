using ProductService.Models;
using ProductService.Data;
namespace ProductService.Repository

{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }
        public List<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }
        public Product Create(Product entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public Product Update(Product entity)
        {
            var product = GetById(entity.Id);
            if (product == null) return null;
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return false;
            _dbContext.Remove(product);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
