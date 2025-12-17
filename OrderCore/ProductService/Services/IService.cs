namespace ProductService.Services
{
    public interface IService<TReceive, TSend>
    {
        Task<TSend> Create(TReceive receive);
        Task<TSend> Update(TReceive receive, int id);
        bool Delete(int id);
        Task<TSend> GetById(int id);
        Task<List<TSend>> GetAll();
    }

}
