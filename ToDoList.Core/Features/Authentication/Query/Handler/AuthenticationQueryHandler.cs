

using MediatR;
using Microsoft.Extensions.Localization;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.Authentication.Query.Models;
using ToDoList.Core.Features.Authentication.Query.Result;
using ToDoList.Core.Resources;
using ToDoList.Data.Results;
using ToDoList.Service.Abstracts;

namespace ToDoList.Core.Features.Authentication.Query.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<GetGoogleAuthUrlQuery, Response<GetGoogleAuthUrlResponseQuery>>,
        IRequestHandler<GetCallBackQuery, Response<OAuthCallBack>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IOAuthService _authenticationService;

        #endregion

        #region Constructors
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                            IOAuthService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }
        #endregion
        public async Task<Response<GetGoogleAuthUrlResponseQuery>> Handle(GetGoogleAuthUrlQuery request, CancellationToken cancellationToken)
        {
            var url = await _authenticationService.GetAuthorizationUrl();
            if (url == null)
            {
                return BadRequest<GetGoogleAuthUrlResponseQuery>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var response = new GetGoogleAuthUrlResponseQuery();
            response.Url = url;
            return Success(response);
        }

        public async Task<Response<OAuthCallBack>> Handle(GetCallBackQuery request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.GetAuthorizationCallBack(request.Code);
            return Success(response);
        }
    }
}
