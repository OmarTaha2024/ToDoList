
using MediatR;
using ToDoList.Core.Bases;
using ToDoList.Data.Results;

namespace ToDoList.Core.Features.Authentication.Query.Models
{
    public class GetCallBackQuery : IRequest<Response<OAuthCallBack>>
    {
        public string Code { get; set; }
    }
}
