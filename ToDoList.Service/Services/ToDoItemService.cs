using ToDoList.Data.Entities;
using ToDoList.Infrustructure.Abstract;
using ToDoList.Infrustructure.Context;
using ToDoList.Service.Abstracts;

namespace ToDoList.Service.Services
{
    public class ToDoItemService : IToDoItemService
    {
        #region Fields
        private readonly IToDoItemRepository _todoitemRepository;
        private readonly ApplicationDbContext _AppDbContext;
        #endregion
        #region Ctor

        public ToDoItemService(ApplicationDbContext AppDbContext, IToDoItemRepository todoitemRepository)
        {
            _todoitemRepository = todoitemRepository;
            _AppDbContext = AppDbContext;
        }


        #endregion
        #region Handle Function
        public async Task<string> AddAsync(ToDoItem todoItem)
        {
            // check if name is exist 
            var todoItemResult = _todoitemRepository.GetTableNoTracking().Where(s => s.Title.Equals(todoItem.Title)).FirstOrDefault();
            if (todoItemResult != null)
                return "Exist";
            // Add Student 
            var result = await _todoitemRepository.AddAsync(todoItem);
            if (result != null) return "Added Successfully";
            return "FailedToAdd";
        }

        public async Task<string> DeleteAsync(ToDoItem todoItem)
        {
            using (var tran = _AppDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _todoitemRepository.DeleteAsync(todoItem);
                    await tran.CommitAsync();
                    return "Sucsess";
                }
                catch
                {
                    await tran.RollbackAsync();
                    return "falied";
                }
            }
        }

        public async Task<List<ToDoItem>> GetAllToDoItems()
        {
            return await Task.FromResult(_todoitemRepository.GetTableAsTracking().ToList());
        }

        public async Task<ToDoItem> GetById(int id)
        {
            var todoitem = _todoitemRepository.GetTableNoTracking().FirstOrDefault(x => x.Id == id);

            return await Task.FromResult(todoitem);
        }

        public async Task<string> UpdateAsync(ToDoItem todoItem)
        {
            using (var tran = _AppDbContext.Database.BeginTransaction())
            {
                try
                {
                    todoItem.UpdatedAt = DateTime.Now;
                    await _todoitemRepository.UpdateAsync(todoItem);
                    await tran.CommitAsync();
                    return "Sucsess";
                }
                catch
                {
                    await tran.RollbackAsync();
                    return "falied";
                }
            }

        }
        #endregion

    }
}
