namespace PostMessengerService.Domain.Dto;

public class PostInformationDto
{
    public string? PostName { get; set; }

    public string Containment { get; set; }

    public DateTime CreationDate { get; set; }

    public string Username { get; set; }
}