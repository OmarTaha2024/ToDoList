using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.BaseController;
using ToDoList.Core.Features.ToDoItem.Commands.Models;
using ToDoList.Core.Features.ToDoItem.Queries.models;
using ToDoList.Core.Filters;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ResourceFilter))]
    public class ToDoItemController : AppControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetList()
        {

            var responce = await Mediator.Send(new GetToDoItemListQuery());
            return NewResult(responce);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid([FromRoute] int id)
        {

            var responce = await Mediator.Send(new GetToDoItemByIdQuery(id));
            return NewResult(responce);
        }
        [HttpGet("offsetpaginatedList")]
        public async Task<IActionResult> GettodoitemoffsetpaginatedList([FromQuery] GetToDoItemoffsetPaginatedListQuery query)
        {
            var responce = await Mediator.Send(query);
            return Ok(responce);
        }
        [HttpGet("CusrorpaginatedList")]
        public async Task<IActionResult> GettodoitemCusrorpaginatedList([FromQuery] GetToDoItemcursorPaginatedListQuery query)
        {
            var responce = await Mediator.Send(query);
            return Ok(responce);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Additem([FromBody] AddTodoItemCommand _command)
        {

            var responce = await Mediator.Send(_command);
            return NewResult(responce);
        }
        [HttpPut]
        public async Task<IActionResult> Updateitem([FromQuery] UpdateTodoItemCommand _command)
        {

            var responce = await Mediator.Send(_command);
            return NewResult(responce);
        }

        [HttpDelete]
        public async Task<IActionResult> Deleteitem([FromQuery] DeleteTodoItemCommand _command)
        {

            var responce = await Mediator.Send(_command);
            return NewResult(responce);
        }
    }
}
