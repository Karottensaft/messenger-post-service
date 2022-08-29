using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostMessengerService.Domain.Models;

public class PostModel
{
    public PostModel()
    {
        Comments = new List<CommentModel>();
        Likes = new List<LikeModel>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; set; }


    [StringLength(64)]
    public string? PostName { get; set; }

    [Required]
    [StringLength(512, MinimumLength = 1)]
    public string Containment { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreationDate { get; set; }

    public string Username { get; set; }

    public List<CommentModel> Comments { get; set; }
    public List<LikeModel> Likes { get; set; }
}