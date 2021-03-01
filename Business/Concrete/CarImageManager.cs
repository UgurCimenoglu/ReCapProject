using Business.Abstract;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file, CarImage entity)
        {
            string ImagePath = FileHelper.Add(file);
            if (ImagePath != null)
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

        public IResult Update(IFormFile file, CarImage entity)
        {
            var image = this.Get(entity.Id);
            if (image.Success && file.Length>0)
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
    }
}
