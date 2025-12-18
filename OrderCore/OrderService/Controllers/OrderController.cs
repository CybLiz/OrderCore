using Microsoft.AspNetCore.Mvc;
using OrderService.DTO;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IService<OrderReceiveDto, OrderSendDto> _service;

    public OrdersController(IService<OrderReceiveDto, OrderSendDto> service)
    {
        _service = service;
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderReceiveDto dto)
    {
        if (dto == null)
            return BadRequest();

        try
        {
            var order = await _service.Create(dto);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // GET 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _service.GetById(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    // GET 
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAll();
        return Ok(orders);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.Delete(id))
            return NotFound();

        return NoContent();
    }
}
