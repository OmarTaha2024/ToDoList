using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.BaseController;
using ToDoList.Core.Features.Authentication.Commands.Models;
using ToDoList.Core.Features.Authentication.Query.Models;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var responce = await Mediator.Send(new GetGoogleAuthUrlQuery());

            return NewResult(responce);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInCommand request)
        {
            var responce = await Mediator.Send(request);

            return NewResult(responce);

        }
    }
}
