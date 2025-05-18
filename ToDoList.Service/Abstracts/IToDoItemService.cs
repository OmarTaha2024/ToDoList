using ToDoList.Data.Entities;

namespace ToDoList.Service.Abstracts
{
    public interface IToDoItemService
    {
        public Task<string> AddAsync(ToDoItem todoItem);
        public Task<string> UpdateAsync(ToDoItem todoItem);
        public Task<string> DeleteAsync(ToDoItem todoItem);
        public Task<List<ToDoItem>> GetAllToDoItems();

        public Task<ToDoItem> GetById(int id);
    }
}
