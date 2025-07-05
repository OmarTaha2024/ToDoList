using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using ToDoList.Core.Bases;
using ToDoList.Core.Features.Authentication.Commands.Models;
using ToDoList.Core.Resources;
using ToDoList.Data.Entities.Identity;
using ToDoList.Data.Helpers;
using ToDoList.Data.Results;
using ToDoList.Service.Abstracts;

namespace ToDoList.Core.Features.Authentication.Commands.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly JwtSettings _jwtSettings;

        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                            UserManager<ApplicationUser> userManager,
                                            SignInManager<ApplicationUser> signInManager,
                                            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }


        #endregion

        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByEmailAsync(request.Email);
            //Return The UserName Not Found
            if (user == null) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.EmailIsNotExist]);

            //try To Sign in 
            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.IsCompletedSuccessfully) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);

            //Generate Token
            var result = await _authenticationService.GetJWTToken(user);
            //return Token 
            return Success(result);
        }
    }
}
