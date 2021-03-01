using Business.Concrete;
using Business.Validator.FluentValidation;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {

        public static void Main(string[] args)
        {

            //CarManager carManager = new CarManager(new EfCarDal());

            //UpdateCar(carManager);

            //GetAllCars(carManager);

            //GetCarsByBrandId(carManager);

            //GetCarsByColorId(carManager);

            //carManager.Delete(new Car { Id = 2});

            //AddCar(carManager);

            //GetCarDetailsDto(carManager);


            BrandManager brandManager = new BrandManager(new EfBrandDal());

            //AddBrand(brandManager);

            //DeleteBrand(brandManager);

            //UpdateBrand(brandManager);

            //GetAllBrands(brandManager);


            ColorManager colorManager = new ColorManager(new EfColorDal());

            //GetAllColor(colorManager);

            //UpdateColor(colorManager);

            //AddColor(colorManager);

            //DeleteColor(colorManager);


            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            //NewMethod(rentalManager);

            //var result2 = rentalManager.FindOneAndUpdate(new Rental { Id = 4}).Message;
            //Console.WriteLine(result2);
            var result3 = rentalManager.getRentalDetailDtos();
            foreach (var item in result3.Data)
            {
                Console.WriteLine(item.CarName + " " + item.UserName + " " + item.RentDate + " " + item.ReturnDate);
            }
        }




        private static void NewMethod(RentalManager rentalManager)
        {
            var result = rentalManager.Rent(new Rental { CarId = 1005, CustomerId = 2, RentDate = DateTime.Now }).Message;
            Console.WriteLine(result);
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        private static void GetCarDetailsDto(CarManager carManager)
        {
            foreach (var cardetail in carManager.GetCarDetailDtos().Data)
            {
                Console.WriteLine("Name : " + cardetail.CarName + " | Brand : " + cardetail.BrandName + " | Color : " + cardetail.ColorName + " | Daily Price  : " + cardetail.DailyPrice);
            }
        }

        private static void DeleteColor(ColorManager colorManager)
        {
            colorManager.Delete(new Color { Id = 3 });
        }

        private static void AddColor(ColorManager colorManager)
        {
            colorManager.Add(new Color { Name = "Yellow" });
        }

        private static void UpdateColor(ColorManager colorManager)
        {
            colorManager.Update(new Color { Id = 1, Name = "Turkuvaz" });
        }

        private static void GetAllColor(ColorManager colorManager)
        {
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.Name);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////


        private static void GetAllBrand(BrandManager brandManager)
        {
            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine(brand.Name);
            }
        }

        private static void UpdateBrand(BrandManager brandManager)
        {
            brandManager.Update(new Brand { Id = 1, Name = "Mercedes Benz" });
        }

        private static void DeleteBrand(BrandManager brandManager)
        {
            brandManager.Delete(new Brand { Id = 3 });
        }

        private static void AddBrands(BrandManager brandManager)
        {
            brandManager.Add(new Brand { Name = "B" });
        }


        ///////////////////////////////////////////////////////////////////////////////////////


        private static void UpdateCar(CarManager carManager)
        {
            carManager.Update(new Car { Id = 3, CarName = "Deneme" });
        }

        private static void AddCar(CarManager carManager)
        {
            Car car1 = new Car { BrandId = 1, CarName = "a", ColorId = 1, DailyPrice = 0, ModelYear = 2005, Description = "Minimum 5 Günlük Kiralanabilir..." };
            carManager.Add(car1);
        }

        private static void GetCarsByColorId(CarManager carManager)
        {
            foreach (var item in carManager.GetCarsByColorId(2).Data)
            {
                Console.WriteLine("color ıdye göre " + item.CarName);
            }
        }

        private static void GetCarsByBrandId(CarManager carManager)
        {
            foreach (var item in carManager.GetCarsByBrandId(1).Data)
            {
                Console.WriteLine("brand ıdye göre " + item.CarName);
            }
        }

        private static void GetAllCars(CarManager carManager)
        {
            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine("ID = " + car.Id + " Car Name = " + car.CarName);
            }
        }
    }
}
