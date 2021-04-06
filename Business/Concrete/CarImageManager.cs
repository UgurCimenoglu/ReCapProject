using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        private readonly string defaultImage = "default.jpg";

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file, CarImage entity)
        {
            string ImagePath = FileHelper.Add(file);
            var result = BusinessRules.Run(CheckIfCarImagesLimitExceded(entity.CarId));
            if (ImagePath != null && result == null)
            {
                entity.Date = DateTime.Now;
                entity.ImagePath = ImagePath;
                _carImageDal.Add(entity);
                return new SuccessResult("Görsel Yüklendi.");
            }
            return new ErrorResult("Görsel Yüklenemedi!!!");
        }

        public IResult Delete(CarImage entity)
        {
            var result = this.Get(entity.Id);
            var isDeleted = FileHelper.Delete(result.Data.ImagePath);
            if (isDeleted.Success)
            {
                _carImageDal.Delete(entity);
                return new SuccessResult("Silindi.");
            }
            return new ErrorResult();
        }

        public IDataResult<CarImage> Get(int CarImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == CarImageId), "Listelendi.");
        }


        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), "Resimler Listelendi.");
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(carId), "Arabaya göre resimler gösterildi.");
        }

        public IResult Update(IFormFile file, CarImage entity)
        {
            var image = this.Get(entity.Id);
            if (image.Success && file.Length > 0)
            {
                var result = FileHelper.Update(file, image.Data.ImagePath);
                var ImageEntity = image.Data;
                ImageEntity.Date = DateTime.Now;
                ImageEntity.ImagePath = result;
                _carImageDal.Update(ImageEntity);
                return new SuccessResult("Resim Güncellendi.");

            }
            return new ErrorResult();
        }


        //Business Rules
        private IResult CheckIfCarImagesLimitExceded(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 5)
            {
                return new ErrorResult("Maksimum Resim Sınıfırna Ulaştı!");
            }
            return new SuccessResult();
        }

        private List<CarImage> CheckIfCarImageNull(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId).Any();
            if (!result)
            {
                return new List<CarImage>
                {
                    new CarImage
                    {
                        CarId = carId,
                        Date= DateTime.Now,
                        ImagePath = defaultImage
                    }
                };
            }
            return _carImageDal.GetAll(i => i.CarId == carId);
        }
    }
}
