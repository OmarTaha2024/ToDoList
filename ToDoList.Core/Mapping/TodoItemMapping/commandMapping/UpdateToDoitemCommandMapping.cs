using ToDoList.Core.Features.ToDoItem.Commands.Models;
using ToDoList.Data.Entities;

namespace ToDoList.Core.Mapping.TodoItemMapping
{
    public partial class ToDoitemProfile
    {
        public void UpdateToDoitemCommandMapping()
        {
            CreateMap<UpdateTodoItemCommand, ToDoItem>();
        }
    }
}
