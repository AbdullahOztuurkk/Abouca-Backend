using Abouca.Application.Constants;
using Abouca.Application.Dto;
using FluentValidation;

namespace Abouca.Application.Validations
{
    public class AddUserValidator:AbstractValidator<UserRegisterDto>
    {
        public AddUserValidator()
        {
            RuleFor(m => m.Password).NotEmpty().WithMessage(Messages.Invalid_Password_Message);
            RuleFor(m => m.Email).NotEmpty().WithMessage(Messages.Invalid_Email_Message);

            RuleFor(m => m.Email).EmailAddress().WithMessage(Messages.Check_Email_Address_Message);
            RuleFor(m => m.Password).MinimumLength(8).WithMessage(Messages.Password_Length_Message);
        }
    }
}
