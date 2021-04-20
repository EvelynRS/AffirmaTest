using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using CarDealership.Models;
using CarDealership.Repositories;
using CarDealership.Helpers;


// Seems to handle more in depth queries and functions

namespace CarDealership.Services
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car GetById(Guid carId);
        void Create(Car carToCreate);
        Car Update(Car updatedCar);
        void Delete(Guid carId);

        List<Car> getByMpg(int low, int high);
        List<Car> getByBody(String bodyIn);
        List<Car> getByMake(String makeIn);
    }


    public class CarService : ICarService
    {
        #region Constants

        private readonly DealershipContext _context;
        private readonly AppSettings _appSettings;

        private readonly IMapper _mapper;

        #endregion

        public CarService(DealershipContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        #region Public Methods

        public List<Car> GetAll()
        {
            var cars = _context.Cars.OrderBy(c => c.Make).ToList();

            return cars;
        }

        public Car GetById(Guid carId)
        {
            var car = _context.Cars.SingleOrDefault(c => c.Id == carId);

            // Error checking
            if (car == null)
            {
                throw new AppException("Car not found");
            }

            return car;
        }

        public void Create(Car carToCreate)
        {
            _context.Cars.Add(carToCreate);
            _context.SaveChanges();
        }

        public Car Update(Car updatedCar)
        {
            if (!_context.Cars.Any(c => c.Id == updatedCar.Id))
            {
                throw new AppException("Car not found");
            }

            _context.Cars.Update(updatedCar);
            _context.SaveChanges();

            return updatedCar;
        }

        public void Delete(Guid carId)
        {
            var car = GetById(carId);

            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }


        // Add methods required here

        // Between min/max mpg
        public List<Car> getByMpg(int low, int high)
        {
            var query = _context.Cars.Where(c => c.MilesPerGallon >= low && c.MilesPerGallon <= high);

            // Is a IQueryable without this
            var cars = query.Select(s => new Car()
            {
                Id = s.Id,
                Make = s.Make,
                Model = s.Model,
                Year = s.Year,
                Price = s.Price,
                Horsepower = s.Horsepower,
                BodyStyle = s.BodyStyle,
                TransmissionType = s.TransmissionType,
                MilesPerGallon = s.MilesPerGallon
            }
            ).ToList();

            // Error checking
            if (cars == null)
            {
                throw new AppException("Body style not found");
            }

            return cars;
        }

        // Specified body type -- Start here, seems less in depth than min/max will use BodyStyle model
        public List<Car> getByBody(String bodyIn)
        {
            var query = _context.Cars.Where(c => c.BodyStyle.GetDisplayName() == bodyIn);

            // Is a IQueryable without this
            var cars = query.Select(s => new Car()
            {
                Id = s.Id,
                Make = s.Make,
                Model = s.Model,
                Year = s.Year,
                Price = s.Price,
                Horsepower = s.Horsepower,
                BodyStyle = s.BodyStyle,
                TransmissionType = s.TransmissionType,
                MilesPerGallon = s.MilesPerGallon
            }
            ).ToList();

            // Error checking
            if (cars == null)
            {
                throw new AppException("Body style not found");
            }

            return cars;
        }


        // Specified make
        public List<Car> getByMake(String MakeIn)
        {
            var query = _context.Cars.Where(c => c.Make == MakeIn);

            // Is a IQueryable without this
            var cars = query.Select(s => new Car()
            {
                Id = s.Id,
                Make = s.Make,
                Model = s.Model,
                Year = s.Year,
                Price = s.Price,
                Horsepower = s.Horsepower,
                BodyStyle = s.BodyStyle,
                TransmissionType = s.TransmissionType,
                MilesPerGallon = s.MilesPerGallon
            }
            ).ToList();

            // Error checking
            if (cars == null)
            {
                throw new AppException("Make not found");
            }

            return cars;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
