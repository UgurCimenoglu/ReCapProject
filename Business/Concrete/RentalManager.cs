using Business.Abstract;
using Business.Validator.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Business;

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
            var result = BusinessRules.Run(checkAvaible(entity));
            if (result != null)
            {
                return result;
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

        public IDataResult<List<RentalDetailDto>> getRentalDetailDtos()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.getRentalDetailDtos(), "Detaylar Listelendi.");
        }


        /*Busines Rules*/

        public IResult checkAvaible(Rental entity)
        {
            var result = _rentalDal.GetAll(r => r.CarId == entity.CarId && (r.ReturnDate == null || r.ReturnDate >= entity.ReturnDate));

            if (result.Count > 0)
            {

                return new ErrorResult("Kiralamak istediğiniz araç şu an aktif olarak başka bir üyemizde kiradadır.");
            }
            return new SuccessResult("Araç Kiralama İslemi Basarili.");
        }
    }
}
