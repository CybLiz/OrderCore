using OrderService.DTO;
using OrderService.Models;
using OrderService.Repository;
using OrderService.Rest;
using ProductService.DTO;
using UserService.DTO;

namespace OrderService.Services;

public class OrderServiceImpl : IService<OrderReceiveDto, OrderSendDto>
{
    private readonly IRepository<Order> _repository;
    private readonly RestClient<UserSendDto> _userClient;
    private readonly RestClient<ProductSendDto> _productClient;

    public OrderServiceImpl(IRepository<Order> repository)
    {
        _repository = repository;

        _userClient = new RestClient<UserSendDto>("http://localhost:5119/api/users/");
        _productClient = new RestClient<ProductSendDto>("http://localhost:5001/api/products/");
    }

    public async Task<OrderSendDto> Create(OrderReceiveDto receive)
    {
        // Appel UserService
        var user = await _userClient.GetRequest(receive.UserId.ToString());

        // Appel ProductService
        List<ProductSendDto> products = new List<ProductSendDto>();
        foreach (var id in receive.ProductIds)
        {
            products.Add(await _productClient.GetRequest(id.ToString()));
        }

        Order order = new Order
        {
            UserId = receive.UserId,
            ProductIds = receive.ProductIds
        };

        order = _repository.Create(order);

        return new OrderSendDto
        {
            Id = order.Id,
            User = user,
            Products = products
        };
    }

    public async Task<OrderSendDto> Update(OrderReceiveDto receive, int id)
    {
        return null;
    }

    public bool Delete(int id)
    {
        return _repository.Delete(id);
    }

    public async Task<OrderSendDto> GetById(int id)
    {
        return null;
    }

    public async Task<List<OrderSendDto>> GetAll()
    {
        return null;
    }
}
