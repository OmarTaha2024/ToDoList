using MediatR;
using ToDoList.Core.Bases;

namespace ToDoList.Core.Features.Appuser.Command.Models
{
    public class LoginCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
