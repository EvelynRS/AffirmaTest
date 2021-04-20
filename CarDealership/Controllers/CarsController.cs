using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CarDealership.Models;
using CarDealership.Helpers;
using CarDealership.Services;
using System.Collections.Generic;
using System;
using CarDealership.DTOs;

// This file is the declarations and definitions for the requests

namespace CarDealership.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarService _carService;

        public CarsController(IMapper mapper, ICarService carService)
        {
            _mapper = mapper;
            _carService = carService;
        }

        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var cars = _carService.GetAll();

                return Ok(_mapper.Map<List<CarDTO>>(cars));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{carId}", Name = "GetById")]
        public IActionResult GetById([FromRoute] Guid carId)
        {
            try
            {
                var car = _carService.GetById(carId);

                return Ok(_mapper.Map<CarDTO>(car));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost(Name = "Create")]
        public IActionResult Create([FromBody] CarDTO carToCreateDto)
        {
            try
            {
                // map dto to entity
                var car = _mapper.Map<Car>(carToCreateDto);

                _carService.Create(car);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut(Name = "Update")]
        public IActionResult Update([FromBody] CarDTO carToUpdateDto)
        {
            try
            {
                // map dto to entity
                var carToUpdate = _mapper.Map<Car>(carToUpdateDto);

                var updatedCar = _mapper.Map<CarDTO>(_carService.Update(carToUpdate));

                return Ok(updatedCar);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{carId}", Name = "Delete")]
        public IActionResult Delete([FromRoute] Guid carId)
        {
            try
            {
                _carService.Delete(carId);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // ADD OPERATIONS HERE

        // Return cars by min/max mpg
        [HttpGet("getmpgrange/{low}/{high}", Name = "getMpgRange")]
        public IActionResult GetByBody([FromRoute] int low, int high)
        {
            try
            {
                var cars = _carService.getByMpg(low, high);

                return Ok(_mapper.Map<List<CarDTO>>(cars));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // Return cars with body type
        [HttpGet("getbybody/{body}", Name = "GetByBody")]
        public IActionResult GetByBody([FromRoute] String body)
        {
            try
            {
                var cars = _carService.getByBody(body);

                return Ok(_mapper.Map<List<CarDTO>>(cars));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // Return cars with make
        [HttpGet("getbymake/{make}", Name = "getByMake")]
        public IActionResult getByMake([FromRoute] String make)
        {

            try
            {
                var cars = _carService.getByMake(make);

                return Ok(_mapper.Map<List<CarDTO>>(cars));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
