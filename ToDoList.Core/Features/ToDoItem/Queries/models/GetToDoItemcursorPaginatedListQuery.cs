using MediatR;
using ToDoList.Core.Features.ToDoItem.Queries.Result;
using ToDoList.Core.Wrappers;

namespace ToDoList.Core.Features.ToDoItem.Queries.models
{
    public class GetToDoItemcursorPaginatedListQuery : IRequest<CursorPaginatedResult<GetToDoItemCursorPaginatedListResponse>>
    {
        public int Cursor { get; set; }
        public int PageSize { get; set; }
    }
}
