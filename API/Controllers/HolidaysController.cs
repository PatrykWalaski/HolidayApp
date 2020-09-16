using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        
        public HolidaysController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<HolidayToReturnDto>>> GetAllOffers([FromQuery] HolidayParams holidayParams)
        {
            var spec = new HolidaysWithSpecifications(holidayParams, false);
            var countSpec = new HolidaysWithSpecifications(holidayParams, true);

            var filteredOffers = await _unitOfWork.Holiday.ListWithSpecAsync(spec);
            var filteredOffersCount = await _unitOfWork.Holiday.CountAsync(countSpec);

            var offersToReturn = _mapper.Map<IReadOnlyList<Holiday>, IReadOnlyList<HolidayToReturnDto>>(filteredOffers);

            return Ok(new Pagination<HolidayToReturnDto>(holidayParams.PageIndex, holidayParams.PageSize, filteredOffersCount, offersToReturn));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HolidayToReturnDto>> GetHoliday(int id)
        {
            var holidayFromDb = await _unitOfWork.Holiday.GetHolidayByIdAsync(id);

            return _mapper.Map<HolidayToReturnDto>(holidayFromDb);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteHoliday(int id)
        {
            if(!await IsAdmin())
                return Unauthorized("Only Admins can delete offers.");

            _unitOfWork.Holiday.Delete(id);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
                return BadRequest("Error while deleting.");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHoliday(int id, Holiday holidayValues)
        {
            if(!await IsAdmin())
                return Unauthorized("Only Admins can update offers.");

            var holidayFromDb = await _unitOfWork.Holiday.GetHolidayByIdAsync(id);

            if (holidayFromDb != null)
            {
                holidayFromDb.HotelName = holidayValues.HotelName;
                holidayFromDb.Description = holidayValues.Description;
                holidayFromDb.Price = holidayValues.Price;
                holidayFromDb.City = holidayValues.City;
                holidayFromDb.MealPlanId = holidayValues.MealPlanId;
                holidayFromDb.TravelAgencyId = holidayValues.TravelAgencyId;
                holidayFromDb.CountryId = holidayValues.CountryId;
                holidayFromDb.DurationOfStay = holidayValues.DurationOfStay;
                holidayFromDb.Stars = holidayValues.Stars;
            }

            _unitOfWork.Holiday.Update(holidayFromDb);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
                return BadRequest("Error while updating.");

            return Ok();

        }

        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<HolidayToReturnDto>>> CreateOffer(Holiday[] offers)
        {
            if(!await IsAdmin())
                return Unauthorized("Only Admins can create offers.");

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

        [HttpGet("Agencies")]
        public async Task<ActionResult<IReadOnlyList<TravelAgency>>> GetTravelAgencies()
        {
            var travelAgencies = await _unitOfWork.Holiday.GetTravelAgenciesAsync();

            return Ok(travelAgencies);
        }

        private async Task<bool> IsAdmin()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            return isAdmin;
        }

    }
}