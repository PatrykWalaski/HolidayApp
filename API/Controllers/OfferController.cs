using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OffersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public OffersController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OfferToReturnDto>>> GetAllOffers()
        {
            var offers = await _context.Offers.Include(x => x.MealPlan).Include(x => x.TravelAgency).Include(x => x.Country).ToListAsync();

            var offersToReturn = _mapper.Map<IReadOnlyList<Offer>, IReadOnlyList<OfferToReturnDto>>(offers);

            return Ok(offersToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<OfferToReturnDto>> CreateOffer(Offer offer)
        {

            var test = await _context.AddAsync(offer);

            var result = await _context.SaveChangesAsync();

            var createdOffer = await _context.Offers.Include(x => x.MealPlan).Include(x => x.TravelAgency).Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == offer.Id);

            var offerToReturn = _mapper.Map<OfferToReturnDto>(createdOffer);

            return offerToReturn;
        }
    }
}