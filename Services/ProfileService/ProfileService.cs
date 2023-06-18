global using AutoMapper;
using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using web_application_task.Data;
using web_application_task.Dtos.Profile;
using web_application_task.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace web_application_task.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private static UserProfile Data = new UserProfile();

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
        .FindFirstValue(ClaimTypes.NameIdentifier)!);


        public async Task<ServiceResponse<GetProfileDto>> AddProfile(AddProfileDto newProfile)
        {
            var serviceResponse = new ServiceResponse<GetProfileDto>();
            


            try
            {
                int id = GetUserId();
                var profileExist = _context.UserProfiles.FirstOrDefaultAsync(u => u.User!.Id == GetUserId());

                if(profileExist.Result is not null)
                    throw new Exception($"Profile Already Exists.");

                var profile = _mapper.Map<UserProfile>(newProfile);

                profile.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
                
                _context.UserProfiles.Add(profile);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetProfileDto>(profile);
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<GetProfileDto>> DeleteProfile()
        {
            var serviceResponse = new ServiceResponse<GetProfileDto>();

            try
            {
            
                var dbUserProfile = await _context.UserProfiles.FirstOrDefaultAsync(c => c.User!.Id == GetUserId());

                if (dbUserProfile is null)
                    throw new Exception($"Profile Not Found.");

                _context.UserProfiles.Remove(dbUserProfile);

                await _context.SaveChangesAsync();

                serviceResponse.Data = null;
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;  
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<GetProfileDto>> GetProfile()
        {

            var serviceResponse = new ServiceResponse<GetProfileDto>();
            var dbUserProfile = await _context.UserProfiles.FirstOrDefaultAsync(c => c.User!.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetProfileDto>(dbUserProfile);
            
            return serviceResponse;
        }


        public async Task<ServiceResponse<GetProfileDto>> UpdateProfile(UpdateProfileDto updatedProfile)
        {
            var serviceResponse = new ServiceResponse<GetProfileDto>();

            try
            {
                
                var dbUserProfile = await _context.UserProfiles.FirstOrDefaultAsync(c => c.User!.Id == GetUserId());

                if (dbUserProfile is null)
                    throw new Exception($"Profile Not Found.");
                

                // Update the profile properties with the values from updatedProfile
                dbUserProfile.Name = updatedProfile.Name;
                dbUserProfile.Address = updatedProfile.Address;
                dbUserProfile.State = updatedProfile.State;
                dbUserProfile.City = updatedProfile.City;
                dbUserProfile.PinCode = updatedProfile.PinCode;
                dbUserProfile.TelePhone = updatedProfile.TelePhone;


                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Map the updated profile to GetProfileDto and assign it to service response data
                serviceResponse.Data = _mapper.Map<GetProfileDto>(dbUserProfile);
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

    }
}

