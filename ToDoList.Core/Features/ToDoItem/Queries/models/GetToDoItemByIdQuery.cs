using MediatR;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.ToDoItem.Queries.Result;

namespace ToDoList.Core.Features.ToDoItem.Queries.models
{
    public class GetToDoItemByIdQuery : IRequest<Response<GetToDoItemResponse>>
    {
        public int Id { get; set; }
        public GetToDoItemByIdQuery(int id)
        {
            Id = id;
        }
    }
}
