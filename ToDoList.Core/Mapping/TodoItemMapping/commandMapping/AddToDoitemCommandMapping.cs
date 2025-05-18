using ToDoList.Core.Features.ToDoItem.Commands.Models;

namespace ToDoList.Core.Mapping.TodoItemMapping
{
    public partial class ToDoitemProfile
    {
        public void AddToDoitemCommandMapping()
        {
            CreateMap<AddTodoItemCommand, Data.Entities.ToDoItem>();
        }
    }
}
