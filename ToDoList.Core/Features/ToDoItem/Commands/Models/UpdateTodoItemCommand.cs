using MediatR;
using ToDoList.Core.Bases;

namespace ToDoList.Core.Features.ToDoItem.Commands.Models
{
    public class UpdateTodoItemCommand : IRequest<Response<string>>
    {
        public int ID { get; set; }
        public bool IsCompleted { get; set; }
    }
}
