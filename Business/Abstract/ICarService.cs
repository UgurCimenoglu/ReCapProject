using Core.Utilities;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByBrandId(int BrandId);
        IDataResult<List<Car>> GetCarsByColorId(int ColorId);
        IResult Add(Car Entity);
        IResult Update(Car Entity);
        IResult Delete(Car Entity);
        IDataResult<List<CarDetailDto>> GetCarDetailDtos();
    }
}
