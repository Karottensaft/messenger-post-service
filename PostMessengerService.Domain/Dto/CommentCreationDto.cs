using System.ComponentModel.DataAnnotations;

namespace PostMessengerService.Domain.Dto;

public class CommentCreationDto
{
    [Required]
    [StringLength(512, MinimumLength = 1)]
    public string CommentContainment { get; set; }

    public string Username { get; set; }

    public int PostId { get; set; }
}