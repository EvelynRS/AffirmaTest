using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CarDealership.Models;
using CarDealership.Helpers;
using CarDealership.Services;
using System.Collections.Generic;
using CarDealership.DTOs;


// This controller just returns what possible values there are as based on the BodyStyle model and TransmissionType model

namespace CarDealership.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LookupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILookupService _lookupService;

        public LookupsController(IMapper mapper, ILookupService lookupService)
        {
            _mapper = mapper;
            _lookupService = lookupService;
        }

        [HttpGet("GetCarBodyStyles", Name = "GetCarBodyStyles")]
        public IActionResult GetCarBodyStyles()
        {
            try
            {
                var bodyStyles = _lookupService.GetCarBodyStyles();
                return Ok(_mapper.Map<List<BodyStyle>, List<LookupDTO>>(bodyStyles));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetCarTransmissionTypes", Name = "GetCarTransmissionTypes")]
        public IActionResult GetCarTransmissionTypes()
        {
            try
            {
                var transmissionTypes = _lookupService.GetCarTransmissionTypes();
                return Ok(_mapper.Map<List<TransmissionType>, List<LookupDTO>>(transmissionTypes));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
