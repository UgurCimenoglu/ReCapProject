using Core.Utilities;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IResult Rent(Rental entity);
        IResult Update(Rental entity);
        IResult Delete(Rental entity);
        IResult FindOneAndUpdate(Rental entity);
        IDataResult<List<RentalDetailDto>> getRentalDetailDtos();
    }
}
