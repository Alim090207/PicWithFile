using ApiFilesIT.Interfaces;
using Microsoft.EntityFrameworkCore;
using PicWithFile.Data;
using PicWithFile.DTOs;
using PicWithFile.Entities;
using PicWithFile.Interfaces;

namespace PicWithFile.Services
{
    public class UsersService : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;
        public UsersService(ApplicationDbContext context, IFileService fileService, IWebHostEnvironment environment)
        {
            _context = context;
            _fileService = fileService;
            _environment = environment;
        }
        public async ValueTask CreateAsync(UsersDto userDto)
        {
            User user = new User();

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.ImageUrl = await _fileService.UploadAsync(userDto.Image);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async ValueTask<IEnumerable<UsersResponseDto>> GetAllAsync()
        {
            IEnumerable<User> users = await _context.Users.ToListAsync();

            List<UsersResponseDto> userResponse = new List<UsersResponseDto>();

            foreach (var i in users)
            {
                if (i != null)
                {
                    userResponse.Add(new UsersResponseDto()
                    {
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        imageBytes = File.ReadAllBytes($@"{_environment.WebRootPath}{i.ImageUrl}")
                    });
                }
            }

            return userResponse;
        }

        public async ValueTask<UsersResponseDto> GetByIdAsync(int UserId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);

            if (user != null)
            {
                UsersResponseDto userResponse = new UsersResponseDto();
                userResponse.FirstName = user.FirstName;
                userResponse.LastName = user.LastName;
                userResponse.imageBytes = File.ReadAllBytes($@"{_environment.WebRootPath}{user.ImageUrl}");

                return userResponse;
            }
            return new UsersResponseDto();
        }
    }
}
