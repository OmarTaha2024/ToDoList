using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities;

namespace ToDoList.Infrustructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

    }
}
