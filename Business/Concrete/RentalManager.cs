using Business.Abstract;
using Business.Utilities;
using Business.Validator.FluentValidation;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Rent(Rental entity)
        {
            var result = _rentalDal.GetAll(r => r.CarId == entity.CarId && r.ReturnDate == null);
            if (result.Count > 0)
            {
                return new ErrorResult("Kiralamak istediğiniz araç şu an aktif olarak başka bir üyemizde kiradadır.");
            }
            _rentalDal.Add(entity);
            return new SuccessResult("Araç Kiralama İslemi Basarili.");
        }

        public IResult Delete(Rental entity)
        {
            _rentalDal.Delete(entity);
            return new SuccessResult("Kiralama islemi silindi.");
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), "Ürünler Listelendi.");
        }

        public IResult Update(Rental entity)
        {
            _rentalDal.Update(entity);
            return new SuccessResult("Kiralama islemi guncellendi.");
        }

        public IResult FindOneAndUpdate(Rental entity)
        {

            var result = _rentalDal.Get(r => r.Id == entity.Id);
            if (result.RentDate != null)
            {
                return new ErrorResult("Teslimat tarihi eklenmiş.");
            }
            result.ReturnDate = DateTime.Now;
            _rentalDal.Update(result);
            return new SuccessDataResult<Rental>("Teslim Tarihi Eklendi.");



        }

        public IDataResult<List<RentalDetailDto>> getRentalDetailDtos()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.getRentalDetailDtos(),"Detaylar Listelendi.");
        }
    }
}
