using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.ToDoItem.Queries.models;
using ToDoList.Core.Features.ToDoItem.Queries.Result;
using ToDoList.Core.Resources;
using ToDoList.Service.Abstracts;

namespace ToDoList.Core.Features.ToDoItem.Queries.Handler
{
    public class TodoQueryHandler : ResponseHandler,
        IRequestHandler<GetToDoItemListQuery, Response<List<GetToDoItemListResponse>>>
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
        #endregion

    }
}
