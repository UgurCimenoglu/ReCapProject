using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Core.DataAccess.EntityFramework;
using Entities.Dto;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetailDtos(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in filter == null ? context.Cars : context.Cars.Where(filter)
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
                             //join image in context.CarImages on car.Id equals image.CarId
                             select new CarDetailDto
                             {
                                 Id = car.Id,
                                 BrandName = brand.Name,
                                 CarName = car.CarName,
                                 ColorName = color.Name,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 ModelYear = car.ModelYear,
                                 ImagePath = context.CarImages.FirstOrDefault(c => c.CarId == car.Id).ImagePath??"default.jpg"
                             };
                return result.ToList();
            }
        }
    }
}
