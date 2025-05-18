using ToDoList.Data.Entities;
using ToDoList.Infrustructure.InfrustructureBases;

namespace ToDoList.Infrustructure.Abstract
{
    public interface IToDoItemRepository : IGenericRepository<ToDoItem>
    {
    }
}
