using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Validator.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }


        //[SecuredOperation("car.add")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car Entity)
        {
            _carDal.Add(Entity);
            return new SuccessResult("Ürün Eklendi.");
        }

        public IResult Delete(Car Entity)
        {
            _carDal.Delete(Entity);
            return new SuccessResult("Silme işlemi başarılı");
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            //if (DateTime.Now.Hour == 02)
            //{
            //    return new ErrorDataResult<List<Car>>("Bakım calismasi...");
            //}
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), "Ürünler Listelenid.");
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailDtos(Expression<Func<Car, bool>> filter = null)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailDtos(filter), "Ürünler Listelendi.");
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int BrandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().Where(c => c.BrandId == BrandId).ToList());
        }

        public IDataResult<List<Car>> GetCarsByColorId(int ColorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().Where(c => c.ColorId == ColorId).ToList());
        }

        public IDataResult<List<Car>> GetCarsByCarId(int CarId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().Where(c => c.Id == CarId).ToList());
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car Entity)
        {
            _carDal.Update(Entity);
            return new SuccessResult("Ürün Güncellendi.");
        }

        [TransactionAspect]
        public IResult TransactionTest(Car Entity)
        {
            if (Equals(Entity.Description!=null))
            {
                _carDal.Add(Entity);
            }

            return null;
        }
    }
}
