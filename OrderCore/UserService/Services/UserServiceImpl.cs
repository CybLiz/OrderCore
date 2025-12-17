using UserService.DTO;
using UserService.Models;
using UserService.Repository;
using UserService.Services;


namespace UserService.Services;

public class UserServiceImpl : IService<UserReceiveDto, UserSendDto>
{
    private readonly IRepository<User> _repository;

    public UserServiceImpl(IRepository<User> repository)
    {
        _repository = repository;
    }

    // Création
    public async Task<UserSendDto> Create(UserReceiveDto receive)
    {
        return await EntityToDto(_repository.Create(DtoToEntity(receive, null)));
    }

    // Mise à jour
    public async Task<UserSendDto> Update(UserReceiveDto receive, int id)
    {
        return await EntityToDto(_repository.Update(DtoToEntity(receive, id)));
    }

    // Suppression
    public bool Delete(int id)
    {
        return _repository.Delete(id);
    }

    // Récupérer par Id
    public async Task<UserSendDto> GetById(int id)
    {
        return await EntityToDto(_repository.GetById(id));
    }

    // Récupérer all
    public async Task<List<UserSendDto>> GetAll()
    {
        List<User> users = _repository.GetAll();
        List<UserSendDto> userDtoSends = new List<UserSendDto>();

        foreach (var user in users)
        {
            userDtoSends.Add(await EntityToDto(user));
        }

        return userDtoSends;
    }

    // Conversion DTO to Entity
    private User DtoToEntity(UserReceiveDto receive, int? id)
    {
        User user = new User()
        {
            Username = receive.Username,
            Email = receive.Email
        };

        if (id != null)
            user.Id = (int)id;

        return user;
    }

    // Conversion Entity to DTO
    private async Task<UserSendDto> EntityToDto(User user)
    {
        if (user == null) return null;

        UserSendDto dto = new UserSendDto()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return dto;
    }
}
