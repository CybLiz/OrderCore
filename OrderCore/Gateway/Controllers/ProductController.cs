using Gateway.Rest;
using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;

namespace Gateway.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly RestClient<ProductSendDto, ProductReceiveDto> _restClient;

    public ProductController()
    {
        _restClient = new RestClient<ProductSendDto, ProductReceiveDto>(
            "http://localhost:5121/api/products"
        );
    }

    [HttpGet]
    public async Task<List<ProductSendDto>> GetAll()
    {
        return await _restClient.GetListRequest("");
    }

    [HttpGet("{id}")]
    public async Task<ProductSendDto> GetById(int id)
    {
        return await _restClient.GetRequest($"/{id}");
    }

    [HttpPost]
    public async Task<ProductSendDto> Create([FromBody] ProductReceiveDto dto)
    {
        return await _restClient.PostRequest("", dto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _restClient.DeleteRequest($"/{id}");
    }
}
