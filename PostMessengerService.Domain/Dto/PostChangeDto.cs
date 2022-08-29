using System.ComponentModel.DataAnnotations;

namespace PostMessengerService.Domain.Dto;

public class PostChangeDto
{
    [StringLength(64)] 
    public string? PostName { get; set; }

    [Required]
    [StringLength(512, MinimumLength = 1)]
    public string Containment { get; set; }
}