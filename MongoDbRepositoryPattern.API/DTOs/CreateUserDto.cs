namespace MongoDbRepositoryPattern.API.DTOs;

public class CreateUserDto
{
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
}
