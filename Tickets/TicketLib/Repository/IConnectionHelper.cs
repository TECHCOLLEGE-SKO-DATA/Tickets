using System.Data.Common;

namespace TicketLib.Repository;
public interface IConnectionHelper<T>
{
    T GetConnection();
}