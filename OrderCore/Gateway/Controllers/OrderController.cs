using Gateway.Rest;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTO;

namespace Gateway.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly RestClient<OrderSendDto, OrderReceiveDto> _restClient;

    public OrderController()
    {
        _restClient = new RestClient<OrderSendDto, OrderReceiveDto>(
            "http://localhost:5002/api/orders"
        );
    }

    // GET all
    [HttpGet]
    public async Task<List<OrderSendDto>> GetAll()
    {
        return await _restClient.GetListRequest("");
    }

    // GET by Id
    [HttpGet("{id}")]
    public async Task<OrderSendDto> GetById(int id)
    {
        return await _restClient.GetRequest($"/{id}");
    }

    // POST
    [HttpPost]
    public async Task<OrderSendDto> Create([FromBody] OrderReceiveDto dto)
    {
        return await _restClient.PostRequest("", dto);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _restClient.DeleteRequest($"/{id}");
    }
}
