using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_application_task.Dtos.Profile;
using web_application_task.Models;
using web_application_task.Services.ProfileService;

namespace web_application_task.Controllers
{
    [Authorize]
    [ApiController]
	[Route("api/user/")]
	public class UserProfileApiController : ControllerBase
	{
        private readonly IProfileService _profileService;

        public UserProfileApiController(IProfileService profileService)
		{
            _profileService = profileService;
        }

        [HttpPost("add-profile")]
        public async Task<ActionResult<ServiceResponse<GetProfileDto>>> CreateProfile(AddProfileDto newProfile)
        {
            return Ok(await _profileService.AddProfile(newProfile));
        }

        [HttpGet("view-profile")]
        public async Task<ActionResult<ServiceResponse<GetProfileDto>>> GetProfile()
        {
            return Ok(await _profileService.GetProfile());
        }

        [HttpPut("update-profile")]
        public async Task<ActionResult<ServiceResponse<GetProfileDto>>> UpdateProfile(UpdateProfileDto updateProfile)
        {
            var response = await _profileService.UpdateProfile(updateProfile);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete-profile")]
        public async Task<ActionResult<ServiceResponse<GetProfileDto>>> DeleteProfile()
        {
            var response = await _profileService.DeleteProfile();
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}

