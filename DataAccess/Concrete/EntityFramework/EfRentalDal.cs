using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectContext>, IRentalDal
    {
        public List<RentalDetailDto> getRentalDetailDtos()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from rental in context.Rentals
                             join customer in context.Customers on rental.CustomerId equals customer.Id
                             join car in context.Cars on rental.CarId equals car.Id
                             join user in context.Users on customer.UserId equals user.Id
                             select new RentalDetailDto { CarName = car.CarName,UserName= user.FirstName+" "+user.LastName, RentDate = rental.RentDate, ReturnDate = rental.ReturnDate };
                return result.ToList();
            }
        }

        public override void Add(Rental entity)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                entity.RentDate = DateTime.Now;
                var AddedEntity = context.Entry(entity);
                AddedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
