using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Validator.FluentValidation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand entity)
        {

            _brandDal.Add(entity);
            return new SuccessResult("Marka Eklendi.");

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
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult("Marka Güncellendi.");
        }
    }
}
