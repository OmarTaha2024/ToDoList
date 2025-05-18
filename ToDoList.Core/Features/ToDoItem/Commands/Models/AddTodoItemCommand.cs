using MediatR;
using ToDoList.Core.Bases;

namespace ToDoList.Core.Features.ToDoItem.Commands.Models
{
    public class AddTodoItemCommand : IRequest<Response<string>>
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
