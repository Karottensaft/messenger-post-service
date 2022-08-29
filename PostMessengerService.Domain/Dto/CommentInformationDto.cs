namespace PostMessengerService.Domain.Dto;

public class CommentInformationDto
{
    public string CommentContainment { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; }

    public string Username { get; set; }

    public int PostId { get; set; }
}