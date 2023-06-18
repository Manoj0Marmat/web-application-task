using System;
using web_application_task.Dtos.Profile;
using web_application_task.Models;

namespace web_application_task
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
            CreateMap<UserProfile, GetProfileDto>();
            CreateMap<AddProfileDto, UserProfile>();
            CreateMap<UpdateProfileDto, GetProfileDto>();
            CreateMap<GetProfileDto, UpdateProfileDto>();
        }
	}
}

