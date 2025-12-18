using ProductService.DTO;
using UserService.DTO;

namespace OrderService.DTO;

public class OrderSendDto
{
    public int Id { get; set; }
    public UserSendDto User { get; set; }
    public List<ProductSendDto> Products { get; set; }
}
