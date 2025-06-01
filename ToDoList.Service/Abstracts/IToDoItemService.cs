using ToDoList.Data.Entities;
using ToDoList.Data.Enums;

namespace ToDoList.Service.Abstracts
{
    public interface IToDoItemService
    {
        public Task<string> AddAsync(ToDoItem todoItem);
        public IQueryable<ToDoItem> GettodoitemQueryableList();
        public Task<string> UpdateAsync(ToDoItem todoItem);
        public Task<string> DeleteAsync(ToDoItem todoItem);
        public Task<List<ToDoItem>> GetAllToDoItems();
        public IQueryable<ToDoItem> FiltertodoitemPaginatedQueryable(ToDoItemoOrderingEnum orderingEnum, string search);
        public Task<ToDoItem> GetById(int id);
    }
}
