using System;
using web_application_task.Dtos.Profile;
using web_application_task.Models;

namespace web_application_task.Services.ProfileService
{
	public interface IProfileService
	{
        Task<ServiceResponse<GetProfileDto>> GetProfile();
        Task<ServiceResponse<GetProfileDto>> AddProfile(AddProfileDto newCharacter);
        Task<ServiceResponse<GetProfileDto>> UpdateProfile(UpdateProfileDto updatedCharacter);
        Task<ServiceResponse<GetProfileDto>> DeleteProfile();
    }
}

