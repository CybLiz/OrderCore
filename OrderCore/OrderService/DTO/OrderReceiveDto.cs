namespace OrderService.DTO;

public class OrderReceiveDto
{
    public int UserId { get; set; }
    public List<int> ProductIds { get; set; }
}
