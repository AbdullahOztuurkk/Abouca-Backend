using System;
using System.Collections.Generic;
using System.Text;
using Abouca.Application.Constants;
using Abouca.Application.Dto;
using FluentValidation;

namespace Abouca.Application.Validations
{
    public class DeleteInformationValidator:AbstractValidator<InformationDeleteDto>
    {
        public DeleteInformationValidator()
        {
            RuleFor(m => m.UserId).NotEmpty().WithMessage(Messages.Invalid_User_Id_Message);
        }
    }
}
