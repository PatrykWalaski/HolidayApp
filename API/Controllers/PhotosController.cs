using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Helpers;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Interfaces;
using Core.Models;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/holidays/{holidayId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        
        public PhotosController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _unitOfWork.Photo.GetByIdAsync(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForHoliday(int holidayId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if(!await IsAdmin())
                return Unauthorized("Only Admins can add photos.");

            var holidayFromRepo = await _unitOfWork.Holiday.GetHolidayByIdAsync(holidayId);
            if(holidayFromRepo == null)
                return BadRequest("There is no holiday with specified id.");

            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(1600).Height(900).Crop("fill") //.Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Url.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
      
            if (!holidayFromRepo.Photos.Any(p => p.IsMain))
                photo.IsMain = true;

            holidayFromRepo.Photos.Add(photo);

            if (await _unitOfWork.Complete() > 0)
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { holidayId = holidayId, id = photo.Id }, photoToReturn);
            };

            return BadRequest("Could not add the photo");
        }


        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int holidayId, int id)
        {
            if(!await IsAdmin())
                return Unauthorized("Only Admins can change main photos.");

            var holidayFromRepo = await _unitOfWork.Holiday.GetHolidayByIdAsync(holidayId);
            if(holidayFromRepo == null)
                return BadRequest("There is no holiday with specified id.");

            // check if there is a photo with this id in user collection
            if  (!holidayFromRepo.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _unitOfWork.Photo.GetByIdAsync(id);

            if(photoFromRepo.IsMain)
                return BadRequest("This is already main photo");

            var photos = await _unitOfWork.Photo.ListAllAsync();
            var currentMainPhoto = photos.Where(x => x.HolidayId == holidayId).FirstOrDefault(p => p.IsMain == true);
            currentMainPhoto.IsMain = false;

            photoFromRepo.IsMain = true;

            if (await _unitOfWork.Complete() > 0)
                return Ok();

            return BadRequest("Failed updating main photo");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int holidayId, int id)
        {
            if(!await IsAdmin())
                return Unauthorized("Only Admins can delete photos.");

            var holidayFromRepo = await _unitOfWork.Holiday.GetHolidayByIdAsync(holidayId);
            if(holidayFromRepo == null)
                return BadRequest("There is no holiday with specified id.");

            // check if there is a photo with this id in user collection
            if  (!holidayFromRepo.Photos.Any(p => p.Id == id))
                return Unauthorized();
        
            var photoFromRepo = await _unitOfWork.Photo.GetByIdAsync(id);

            if(photoFromRepo.IsMain)
               return BadRequest("You cannot delete ur main photo");

            if(photoFromRepo.PublicID != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicID);
                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                    _unitOfWork.Photo.Delete(photoFromRepo);
            } else 
                _unitOfWork.Photo.Delete(photoFromRepo);

           
            if(await _unitOfWork.Complete() > 0)
                return Ok();

            return BadRequest("Failed to delete the photo");
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