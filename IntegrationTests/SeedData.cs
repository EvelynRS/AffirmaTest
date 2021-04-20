using CarDealership.Models;
using CarDealership.Repositories;
using System;


namespace Web.Api.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(DealershipContext dbContext)
        {

                /*dbContext.Cars.Add(new Car(new Guid("3f7c5757-23f6-4160-8ccb-fc60a741af81")) { Make = "Hyundai", Model = "Genesis", Year = 2014, Price = 35000, Horsepower = 333, BodyStyle = BodyStyles.Coupe, TransmissionType = TransmissionTypes.Manual, MilesPerGallon = 30 });
                dbContext.Cars.Add(new Car() { Make = "Nissan", Model = "Quest", Year = 2008, Price = 6000, Horsepower = 235, BodyStyle = BodyStyles.Minivan, TransmissionType = TransmissionTypes.Automatic, MilesPerGallon = 28 });
                dbContext.Cars.Add(new Car() { Make = "INFINITI", Model = "QX60", Year = 2016, Price = 26500, Horsepower = 295, BodyStyle = BodyStyles.SUV, TransmissionType = TransmissionTypes.CVT, MilesPerGallon = 34 });
                dbContext.Cars.Add(new Car() { Make = "Ford", Model = "Mustang", Year = 2015, Price = 18500, Horsepower = 300, BodyStyle = BodyStyles.Coupe, TransmissionType = TransmissionTypes.Automatic, MilesPerGallon = 26 });
                dbContext.Cars.Add(new Car() { Make = "Toyota", Model = "Corolla", Year = 2017, Price = 13500, Horsepower = 132, BodyStyle = BodyStyles.Sedan, TransmissionType = TransmissionTypes.Automatic, MilesPerGallon = 38 });
                dbContext.Cars.Add(new Car() { Make = "Ford", Model = "F-150", Year = 2021, Price = 75945, Horsepower = 430, BodyStyle = BodyStyles.PickupTruck, TransmissionType = TransmissionTypes.Automatic, MilesPerGallon = 18 });
                */
            dbContext.SaveChanges();
        }
    }
}