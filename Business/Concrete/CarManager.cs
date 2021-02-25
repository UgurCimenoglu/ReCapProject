﻿using Business.Abstract;
using Business.Utilities;
using Business.Validator.FluentValidation;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car Entity)
        {

            if (ValidatorService.Validator(new CarValidator(), Entity))
            {
                _carDal.Add(Entity);
                return new SuccessResult("Ürün Eklendi.");
            }
            else
            {
                Console.WriteLine("Eklenemedi!");
                return new ErrorResult("Ürün Eklenemedi!");
            }

        }

        public IResult Delete(Car Entity)
        {
            _carDal.Delete(Entity);
            return new SuccessResult("Silme işlemi başarılı");
        }

        public IDataResult<List<Car>> GetAll()
        {
            //if (DateTime.Now.Hour == 02)
            //{
            //    return new ErrorDataResult<List<Car>>("Bakım calismasi...");
            //}
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), "Ürünler Listelenid.");
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailDtos()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailDtos(), "Ürünler Listelendi.");
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int BrandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().Where(c => c.BrandId == BrandId).ToList());
        }

        public IDataResult<List<Car>> GetCarsByColorId(int ColorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().Where(c => c.ColorId == ColorId).ToList());
        }

        public IResult Update(Car Entity)
        {
            _carDal.Update(Entity);
            return new SuccessResult("Ürün Güncellendi.");
        }
    }
}