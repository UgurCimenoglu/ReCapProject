using Business.Abstract;
using Business.Utilities;
using Business.Validator.FluentValidation;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand entity)
        {
            if (ValidatorService.Validator(new BrandValidator(), entity))
            {
                _brandDal.Add(entity);
                return new SuccessResult("Marka Eklendi.");
            }
            else
            {
                Console.WriteLine("Marka Eklenemedi...");
                return new ErrorResult("Hata!");

            }
        }

        public IResult Delete(Brand entity)
        {
            _brandDal.Delete(entity);
            return new SuccessResult("Marka Silindi.");
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), "Markalar Listelendi.");
        }

        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult("Marka Güncellendi.");
        }
    }
}
