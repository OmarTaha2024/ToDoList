using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.BaseController;
using ToDoList.Core.Features.ToDoItem.Commands.Models;
using ToDoList.Core.Features.ToDoItem.Queries.models;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {

            var responce = await Mediator.Send(new GetToDoItemListQuery());
            return NewResult(responce);
        }
        [HttpGet("offsetpaginatedList")]
        public async Task<IActionResult> GettodoitemoffsetpaginatedList([FromQuery] GetToDoItemoffsetPaginatedListQuery query)
        {
            var responce = await Mediator.Send(query);
            return Ok(responce);
        }
        [HttpPost]
        public async Task<IActionResult> Additem([FromBody] AddTodoItemCommand _command)
        {

            var responce = await Mediator.Send(_command);
            return NewResult(responce);
        }
        [HttpPut]
        public async Task<IActionResult> Updateitem([FromBody] UpdateTodoItemCommand _command)
        {

            var responce = await Mediator.Send(_command);
            return NewResult(responce);
        }

        [HttpDelete]
        public async Task<IActionResult> Deleteitem([FromBody] DeleteTodoItemCommand _command)
        {

            var responce = await Mediator.Send(_command);
            return NewResult(responce);
        }
    }
}
