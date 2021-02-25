using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validator.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r=>r.Id).NotNull().WithMessage("Lütfen Geçerli Bir Teslimat Tarihi Giriniz.");

        }
    }
}
