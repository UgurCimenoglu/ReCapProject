using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICreditCardService 
    {
        IResult Add(CreditCard entity);
        IResult Update(CreditCard entity);
        IResult Delete(CreditCard entity);
        IDataResult<List<CreditCard>> GetAll();
        IDataResult<List<CreditCard>> GetByCustomerId(int customerId);
    }
}
