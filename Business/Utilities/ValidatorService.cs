using Core.Entities;
using Entities.Concrete;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities
{
    public static class ValidatorService
    {
        public static bool Validator(IValidator validator, IEntity entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                ((List<FluentValidation.Results.ValidationFailure>)result.Errors).ForEach(e => Console.WriteLine(e.ErrorMessage));
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
