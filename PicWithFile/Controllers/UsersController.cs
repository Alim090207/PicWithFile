using ApiFilesIT.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PicWithFile.DTOs;

namespace PicWithFile.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;

        public UsersController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync([FromForm] UsersDto userModel)
        {
            await _userRepository.CreateAsync(userModel);
            return Ok("Created");
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetByIdAsync(int Id)
        {
            UsersResponseDto user = await _userRepository.GetByIdAsync(Id);

            return Ok(new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            });
        }

        [HttpGet]
        public async ValueTask<FileContentResult> GetImageByUserIdAsync(int UserId)
        {
            UsersResponseDto user = await _userRepository.GetByIdAsync(UserId);

            return File(user.imageBytes, "image/png");
        }
    }
}
