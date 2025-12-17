using ProductService.DTO;
using ProductService.Models;
using ProductService.Repository;

namespace ProductService.Services
{
    public class ProductServiceImpl : IService<ProductReceiveDto, ProductSendDto>
    {
        private readonly IRepository<Product> _repository;

        public ProductServiceImpl(IRepository<Product> repository)
        {
            _repository = repository;
        }

        // Création
        public async Task<ProductSendDto> Create(ProductReceiveDto receive)
        {
            return await EntityToDto(_repository.Create(DtoToEntity(receive, null)));
        }

        // Mise à jour
        public async Task<ProductSendDto> Update(ProductReceiveDto receive, int id)
        {
            return await EntityToDto(_repository.Update(DtoToEntity(receive, id)));
        }

        // Suppression
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        // Récupérer par Id
        public async Task<ProductSendDto> GetById(int id)
        {
            return await EntityToDto(_repository.GetById(id));
        }

        // Récupérer tous
        public async Task<List<ProductSendDto>> GetAll()
        {
            List<Product> products = _repository.GetAll();
            List<ProductSendDto> productDtoSends = new();

            foreach (var product in products)
            {
                productDtoSends.Add(await EntityToDto(product));
            }

            return productDtoSends;
        }

        // Conversion DTO → Entity
        private Product DtoToEntity(ProductReceiveDto receive, int? id)
        {
            Product product = new()
            {
                Name = receive.Name,
                Description = receive.Description,
                Price = receive.Price,
            };

            if (id != null)
            {
                product.Id = id.Value;
            }

            return product;
        }

        // Conversion Entity → DTO
        private async Task<ProductSendDto> EntityToDto(Product product)
        {
            if (product == null) return null;

            ProductSendDto productSendDto = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };

            return productSendDto;
        }
    }
}
