using MediatR;
using ToDoList.Core.Bases;

namespace ToDoList.Core.Features.ToDoItem.Commands.Models
{
    public class DeleteTodoItemCommand : IRequest<Response<string>>
    {
        public int ID { get; set; }
    }
}
