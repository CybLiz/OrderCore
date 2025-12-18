using Gateway.Rest;
using Microsoft.AspNetCore.Mvc;
using UserService.DTO;

namespace Gateway.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly RestClient<UserSendDto, UserReceiveDto> _restClient;

    public UserController()
    {
        _restClient = new RestClient<UserSendDto, UserReceiveDto>(
            "http://localhost:5119/api/users"
        );
    }

    [HttpGet]
    public async Task<List<UserSendDto>> GetAll()
    {
        return await _restClient.GetListRequest("");
    }

    [HttpGet("{id}")]
    public async Task<UserSendDto> GetById(int id)
    {
        return await _restClient.GetRequest($"/{id}");
    }

    [HttpPost]
    public async Task<UserSendDto> Create([FromBody] UserReceiveDto dto)
    {
        return await _restClient.PostRequest("", dto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _restClient.DeleteRequest($"/{id}");
    }
}
