using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validator.FluentValidation
{
    class BrandValidator: AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Araç İsmi Minimum 2 Harften Oluşmak Zorundadır...");
        }
    }
}
