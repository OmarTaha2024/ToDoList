using ToDoList.Data.Entities.Identity;
using ToDoList.Infrustructure.InfrustructureBases;

namespace ToDoList.Infrustructure.Abstract
{
    public interface IRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
    }
}
