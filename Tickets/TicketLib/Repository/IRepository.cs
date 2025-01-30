using TicketLib.Models;
namespace TicketLib.Repository;

public interface IRepository<T> where T : BaseModel
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T model);
    void Update(T model);
    void Delete(int id);
}
