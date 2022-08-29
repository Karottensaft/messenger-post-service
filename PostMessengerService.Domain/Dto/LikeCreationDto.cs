using System.ComponentModel.DataAnnotations;

namespace PostMessengerService.Domain.Dto;

public class LikeCreationDto
{
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreationDate { get; set; }

    public int PostId { get; set; }

    public string Username { get; set; }
}