using MediatR;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.ToDoItem.Queries.Result;

namespace ToDoList.Core.Features.ToDoItem.Queries.models
{
    public class GetToDoItemListQuery : IRequest<Response<List<GetToDoItemListResponse>>>
    {
    }
}
