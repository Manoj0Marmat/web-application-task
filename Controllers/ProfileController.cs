using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using web_application_task.Dtos.Profile;
using web_application_task.Services.ProfileService;

namespace web_application_task.Controllers
{
    [Authorize]
    [Route("User/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serviceResponse = await _profileService.GetProfile();

            if (!serviceResponse.Success)
            {
                // Handle error case
                return View("Error", serviceResponse.Message);
            }

            var profileDto = serviceResponse.Data;

            return View(profileDto);
        }

        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(AddProfileDto newProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(newProfile);
            }

            var serviceResponse = await _profileService.AddProfile(newProfile);

            if (!serviceResponse.Success)
            {
                // Handle error case
                return View("Error", serviceResponse.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit()
        {
            var serviceResponse = await _profileService.GetProfile();

            if (!serviceResponse.Success)
            {
                // Handle error case
                return View("Error", serviceResponse.Message);
            }

            var profileDto = serviceResponse.Data;
            var updateProfileDto = _mapper.Map<UpdateProfileDto>(profileDto);

            return View(updateProfileDto);
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(UpdateProfileDto updatedProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedProfile);
            }

            var serviceResponse = await _profileService.UpdateProfile(updatedProfile);

            if (!serviceResponse.Success)
            {
                // Handle error case
                return View("Error", serviceResponse.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete()
        {
            var serviceResponse = await _profileService.DeleteProfile();

            if (!serviceResponse.Success)
            {
                // Handle error case
                return View("Error", serviceResponse.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
