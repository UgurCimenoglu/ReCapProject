using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        private ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creeCardDal)
        {
            _creditCardDal = creeCardDal;
        }

        public IResult Add(CreditCard entity)
        {
            _creditCardDal.Add(entity);
            return new SuccessResult("Card Eklendi.");
        }

        public IResult Delete(CreditCard entity)
        {
            _creditCardDal.Delete(entity);
            return new SuccessResult("Card Silindi.");
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            var result = _creditCardDal.GetAll();
            return new SuccessDataResult<List<CreditCard>>(result, "Listelendi.");
        }

        public IDataResult<List<CreditCard>> GetByCustomerId(int customerId)
        {
            var result = _creditCardDal.GetAll(c => c.CustomerId == customerId);
            return new SuccessDataResult<List<CreditCard>>(result, "Listelendi.");
        }

        public IResult Update(CreditCard entity)
        {
            _creditCardDal.Update(entity);
            return new SuccessResult("Güncellendi.");
        }
    }
}
