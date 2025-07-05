using MediatR;
using Microsoft.Extensions.Localization;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.Appuser.Command.Models;
using ToDoList.Core.Resources;
using ToDoList.Service.Abstracts;

namespace ToDoList.Core.Features.Appuser.Command.Handler
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<LoginCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly IAuthenticationService _userService;
        #endregion
        #region CTOR

        public UserCommandHandler(IAuthenticationService userService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _sharedResources = stringLocalizer;
            _userService = userService;
        }

        public async Task<Response<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var result = await _userService.LoginAsync(request.Email, request.Password);
            if (result == "Not Found") return BadRequest<string>("Not Found");
            else if (result == "Success")
                return Success(result);
            return BadRequest<string>(result);
        }
        #endregion
    }
}
