using FluentValidation;
using Microsoft.Extensions.Localization;
using ToDoList.Core.Features.ToDoItem.Commands.Models;
using ToDoList.Core.Resources;

namespace ToDoList.Core.Features.ToDoItem.Commands.Validators
{
    public class DeleteTodoItemValidator :
     AbstractValidator<DeleteTodoItemCommand>
    {

        #region Feilds
        private readonly IStringLocalizer<SharedResources> _Localizer;


        #endregion
        #region ctor
        public DeleteTodoItemValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _Localizer = stringLocalizer;

            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.ID)
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.Required]);


        }
        public void ApplyCustomValidationRules()
        {


        }
        #endregion
    }
}
