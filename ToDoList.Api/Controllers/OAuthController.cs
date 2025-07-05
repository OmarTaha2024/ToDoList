using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.BaseController;
using ToDoList.Core.Features.Authentication.Query.Models;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : AppControllerBase
    {
        [HttpPost("google/callback")]
        public async Task<IActionResult> Login([FromBody] GetCallBackQuery request)
        {
            var responce = await Mediator.Send(request);

            return NewResult(responce);

        }
    }
}
