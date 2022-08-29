using System.ComponentModel.DataAnnotations;

namespace PostMessengerService.Domain.Dto;

public class CommentChangeDto
{
    [Required]
    [StringLength(512, MinimumLength = 1)]
    public string CommentContainment { get; set; }
}