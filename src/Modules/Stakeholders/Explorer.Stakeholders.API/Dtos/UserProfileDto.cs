namespace Explorer.Stakeholders.API.Dtos;

public class UserProfileDto
{
    public int Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; init; }
    public string? ProfileImage { get; set; }
    public string? Bio {  get; set; }
    public string? Quote { get; set; }

}
