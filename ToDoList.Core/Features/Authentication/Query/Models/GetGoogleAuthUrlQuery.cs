using MediatR;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.Authentication.Query.Result;

namespace ToDoList.Core.Features.Authentication.Query.Models
{
    public class GetGoogleAuthUrlQuery : IRequest<Response<GetGoogleAuthUrlResponseQuery>>
    {
    }
}
