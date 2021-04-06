using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(color => color.Name).MinimumLength(2).WithMessage("Renk adı 2 karakterden zuzn olmalıdır.");
        }
    }
}
