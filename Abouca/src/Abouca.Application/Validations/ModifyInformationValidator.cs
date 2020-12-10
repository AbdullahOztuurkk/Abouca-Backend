using System;
using System.Collections.Generic;
using System.Text;
using Abouca.Application.Constants;
using Abouca.Domain.Information;
using FluentValidation;

namespace Abouca.Application.Validations
{
    public class ModifyInformationValidator:AbstractValidator<Information>
    {
        public ModifyInformationValidator()
        {
            RuleFor(m => m.User).NotEmpty().WithMessage(Messages.Invalid_User_Message);
            RuleFor(m => m.UserId).NotEmpty().WithMessage(Messages.Invalid_User_Id_Message);
        }
    }
}
