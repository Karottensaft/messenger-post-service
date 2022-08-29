using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostMessengerService.Domain.Models;

public class CommentModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }

    [Required]
    [StringLength(512, MinimumLength = 1)]
    public string CommentContainment { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreationDate { get; set; }

    public string Username { get; set; }

    [ForeignKey("PostModel")]
    public int PostId { get; set; }

    public PostModel Post { get; set; }
}