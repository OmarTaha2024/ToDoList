using MediatR;
using ToDoList.Core.Features.ToDoItem.Queries.Result;
using ToDoList.Core.Wrappers;

namespace ToDoList.Core.Features.ToDoItem.Queries.models
{
    public class GetToDoItemoffsetPaginatedListQuery : IRequest<OffsetPaginatedResult<GetToDoItemoffsetPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
