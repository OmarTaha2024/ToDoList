using MediatR;
using ToDoList.Core.Bases;
using ToDoList.Data.Results;

namespace ToDoList.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
