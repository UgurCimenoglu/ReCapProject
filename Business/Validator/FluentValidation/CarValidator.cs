using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validator.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage("Aracın Günlük Fiyatını 0'dan Büyük Girmelisiniz...");
            RuleFor(c => c.CarName).MinimumLength(2).WithMessage("Arac ismi minimum 2 karakter olmalıdır...");
        }
    }
}
