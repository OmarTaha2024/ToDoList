using MediatR;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.ToDoItem.Queries.Result;
using ToDoList.Data.Abstract;

namespace ToDoList.Core.Features.ToDoItem.Queries.models
{
    public class GetToDoItemListQuery : IRequest<Response<List<GetToDoItemListResponse>>>, ICacheable
    {
        public string CacheKey => $"AllItems";
        public TimeSpan CacheDuration => TimeSpan.FromMinutes(1);
    }
}
