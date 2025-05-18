using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;
using ToDoList.Infrustructure.Abstract;
using ToDoList.Infrustructure.Context;
using ToDoList.Infrustructure.InfrustructureBases;

namespace ToDoList.Infrustructure.Represatories
{
    public class ToDoItemRepository : GenericRepository<ToDoItem>, IToDoItemRepository
    {
        #region Fields
        protected readonly DbSet<ToDoItem> _item;

        #endregion
        #region Ctor

        public ToDoItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _item = dbContext.Set<ToDoItem>();
        }
        #endregion

    }

}
