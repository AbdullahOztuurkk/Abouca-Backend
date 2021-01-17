using System;
using System.Collections.Generic;
using System.Text;
using Abouca.Application.Constants;
using Abouca.Domain.User;
using FluentValidation;

namespace Abouca.Application.Validations
{
    public class ModifyUserValidator:AbstractValidator<User>
    {
        public ModifyUserValidator()
        {
            RuleFor(m => m.Id).NotEmpty().WithMessage(Messages.Invalid_Id_Message);
        }
    }
}
