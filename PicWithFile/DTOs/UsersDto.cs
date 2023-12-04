namespace PicWithFile.DTOs
{
    public class UsersDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public IFormFile Image { get; set; } = default!;
    }
}
