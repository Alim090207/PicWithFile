using PicWithFile.DTOs;

namespace ApiFilesIT.Interfaces
{
    public interface IUsersRepository
    {
        ValueTask CreateAsync(UsersDto userDto);
        ValueTask<UsersResponseDto> GetByIdAsync(int UserId);
        ValueTask<IEnumerable<UsersResponseDto>> GetAllAsync();
    }
}
