using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.ToDoItem.Queries.models;
using ToDoList.Core.Features.ToDoItem.Queries.Result;
using ToDoList.Core.Resources;
using ToDoList.Core.Wrappers;
using ToDoList.Service.Abstracts;

namespace ToDoList.Core.Features.ToDoItem.Queries.Handler
{
    public class TodoQueryHandler : ResponseHandler,
        IRequestHandler<GetToDoItemListQuery, Response<List<GetToDoItemListResponse>>>,
        IRequestHandler<GetToDoItemoffsetPaginatedListQuery, OffsetPaginatedResult<GetToDoItemoffsetPaginatedListResponse>>,
        IRequestHandler<GetToDoItemcursorPaginatedListQuery, CursorPaginatedResult<GetToDoItemCursorPaginatedListResponse>>
    {
        #region Fields
        private readonly IToDoItemService _todoserv;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;


        #endregion
        #region CTOR
        public TodoQueryHandler(IToDoItemService todoserv, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _todoserv = todoserv;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;


        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetToDoItemListResponse>>> Handle(GetToDoItemListQuery request, CancellationToken cancellationToken)
        {
            var todolist = await _todoserv.GetAllToDoItems();
            var todolistMapper = _mapper.Map<List<GetToDoItemListResponse>>(todolist);
            var Result = Success(todolistMapper);
            Result.Meta = new { count = todolistMapper.Count() };
            return Result;

        }

        public async Task<OffsetPaginatedResult<GetToDoItemoffsetPaginatedListResponse>> Handle(GetToDoItemoffsetPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Data.Entities.ToDoItem, GetToDoItemoffsetPaginatedListResponse>> ex = ex => new GetToDoItemoffsetPaginatedListResponse(ex.Id, ex.Title, ex.IsCompleted);
            var filterqueryable = _todoserv.FiltertodoitemPaginatedQueryable(request.OrderBy, request.Search);
            var paginatedlist = await filterqueryable.Select(ex).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedlist;
        }

        public async Task<CursorPaginatedResult<GetToDoItemCursorPaginatedListResponse>> Handle(GetToDoItemcursorPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Data.Entities.ToDoItem, GetToDoItemCursorPaginatedListResponse>> ex = ex => new GetToDoItemCursorPaginatedListResponse(ex.Id, ex.Title, ex.IsCompleted);
            var queryable = _todoserv.GettodoitemQueryableList();
            var paginatedlist = await queryable.Select(ex).ToPaginatedListAsync(request.Cursor, request.PageSize);
            return paginatedlist;
        }
        #endregion

    }
}
