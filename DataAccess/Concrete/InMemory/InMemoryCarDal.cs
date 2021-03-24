using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            //_cars = new List<Car>
            //{
            //    new Car{Id=1, BrandId=1,ColorId=3,ModelYear=2020,DailyPrice=150.79,Description="Kiralanabilir durumda..."},
            //    new Car{Id=2, BrandId=2,ColorId=1,ModelYear=2010,DailyPrice=123.79,Description="Kiralanabilir durumda..."},
            //    new Car{Id=3, BrandId=2,ColorId=2,ModelYear=2009,DailyPrice=111.00,Description="Kiralanabilir durumda..."},
            //    new Car{Id=4, BrandId=3,ColorId=2,ModelYear=2017,DailyPrice=150.00,Description="Kiralanabilir durumda..."},
            //    new Car{Id=5, BrandId=1,ColorId=1,ModelYear=2015,DailyPrice=215.44,Description="Kiralanabilir durumda..."},
            //};
        }

        public void Add(Car car)
        {
            _cars.Add(car);

        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars.ToList();
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.Id == id).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Description = car.Description;
        }
        public void Deneme()
        {
            Console.WriteLine("Deneme");
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetailDtos()
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetailDtos(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
