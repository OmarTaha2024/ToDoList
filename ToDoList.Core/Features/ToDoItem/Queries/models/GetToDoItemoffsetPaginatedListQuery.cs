using MediatR;
using ToDoList.Core.Features.ToDoItem.Queries.Result;
using ToDoList.Core.Wrappers;
using ToDoList.Data.Enums;

namespace ToDoList.Core.Features.ToDoItem.Queries.models
{
    public class GetToDoItemoffsetPaginatedListQuery : IRequest<OffsetPaginatedResult<GetToDoItemoffsetPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ToDoItemoOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
