using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using CarDealership.Models;
using CarDealership.Repositories;
using CarDealership.Helpers;

// Works with LookupsController to provide list of possible body and transmission types

namespace CarDealership.Services
{
    public interface ILookupService
    {
        List<BodyStyle> GetCarBodyStyles();
        List<TransmissionType> GetCarTransmissionTypes();
    }


    public class LookupService : ILookupService
    {
        #region Constants

        private readonly DealershipContext _context;
        private readonly AppSettings _appSettings;

        private readonly IMapper _mapper;

        #endregion

        public LookupService(DealershipContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        #region Public Methods

        public List<BodyStyle> GetCarBodyStyles()
        {
            var bodyStyles = _context.BodyStyles.ToList();
            
            return bodyStyles;
        }

        public List<TransmissionType> GetCarTransmissionTypes()
        {
            var transmissionTypes = _context.TransmissionTypes.ToList();

            return transmissionTypes;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
