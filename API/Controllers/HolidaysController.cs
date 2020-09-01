using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class HolidaysController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;
        public HolidaysController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<HolidayToReturnDto>>> GetAllOffers([FromQuery]HolidayParams holidayParams)
        {
            var spec = new HolidaysWithSpecifications(holidayParams);
            
            var filteredOffers = await _unitOfWork.Holiday.ListWithSpecAsync(spec);

            var offersToReturn = _mapper.Map<IReadOnlyList<Offer>, IReadOnlyList<HolidayToReturnDto>>(filteredOffers);

            return Ok(offersToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<HolidayToReturnDto>>> CreateOffer(Offer[] offers)
        {
            foreach (var offer in offers)
            {
               _unitOfWork.Holiday.Add(offer); 
            }

            var result = await _unitOfWork.Complete();

            if (result <= 0)
                return BadRequest("Offer creation error");

            var createdOffer = await _unitOfWork.Holiday.GetHolidaysAsync();

            var offerToReturn = _mapper.Map<IReadOnlyList<HolidayToReturnDto>>(createdOffer);

            return Ok(offerToReturn);
        }

        // ======================================================

        [HttpGet("MealPlans")]
        public async Task<ActionResult<IReadOnlyList<MealPlan>>> GetMealPlans()
        {
            var mealPlans = await _unitOfWork.Holiday.GetMealPlansAsync();

            return Ok(mealPlans);
        }

        [HttpGet("Countries")]
        public async Task<ActionResult<Country>> GetCountries()
        {
            var countries = await _unitOfWork.Holiday.GetCountriesAsync();

            return Ok(countries);
        }

        [HttpGet("string:Agencies")]
        public async Task<ActionResult<IReadOnlyList<TravelAgency>>> GetTravelAgencies()
        {
            var travelAgencies = await _unitOfWork.Holiday.GetTravelAgenciesAsync();

            return Ok(travelAgencies);
        }

        // ======================================================

        // [HttpGet("Agencies")]
        // public async Task<ActionResult<TravelAgency>> GetTravelAgencyByIds([FromQuery] int[] listOfIds)
        // {
        //     var travelAgenciesByIds = new List<TravelAgency>();

        //     foreach (var id in listOfIds)
        //     {
        //         travelAgenciesByIds.Add(await _context.TravelAgencies.FindAsync(id));
        //     }

        //     return Ok(travelAgenciesByIds);
        // }

    }
}