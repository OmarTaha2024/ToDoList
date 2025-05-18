using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.ToDoItem.Commands.Models;
using ToDoList.Core.Resources;
using ToDoList.Service.Abstracts;

namespace ToDoList.Core.Features.ToDoItem.Commands.Handler
{
    public class TodoitemHandler : ResponseHandler,
        IRequestHandler<AddTodoItemCommand, Response<string>>,
        IRequestHandler<UpdateTodoItemCommand, Response<string>>,
        IRequestHandler<DeleteTodoItemCommand, Response<string>>
    {



        #region Fields
        private readonly IToDoItemService _todoitemService;
        private readonly IMapper _imapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;


        #endregion
        #region Ctor
        public TodoitemHandler(IToDoItemService todoitemService, IMapper imapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {

            _stringLocalizer = stringLocalizer;
            _imapper = imapper;
            _todoitemService = todoitemService;

        }
        #endregion
        #region Handling Method 
        public async Task<Response<string>> Handle(AddTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoitemmapper = _imapper.Map<Data.Entities.ToDoItem>(request);
            // Add using Service 
            var result = await _todoitemService.AddAsync(todoitemmapper);
            //check condition
            if (result == "Added Successfully")
                return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
            else
                return BadRequest<string>();
            //return Responce 
        }

        public async Task<Response<string>> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoitem = await _todoitemService.GetById(request.ID);
            if (todoitem == null) return BadRequest<string>();

            var todoitemmapper = _imapper.Map(request, todoitem);
            // Add using Service 
            var result = await _todoitemService.UpdateAsync(todoitemmapper);
            //check condition
            if (result == "Sucsess")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
            else
                return BadRequest<string>();
            //return Responce 
        }

        public async Task<Response<string>> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoitem = await _todoitemService.GetById(request.ID);
            if (todoitem == null) return BadRequest<string>();
            var result = await _todoitemService.DeleteAsync(todoitem);
            //check condition
            if (result == "Sucsess")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
            else
                return BadRequest<string>();
            //return Responce 
        }
        #endregion
    }

}
