using System.ComponentModel.DataAnnotations;
using WebWithDotNet.Resources.Message;
namespace WebWithDotNet.Models;

public class Category
{
    public int Id {get; set;}
    [Required(ErrorMessage = "required")]
    [StringLength(10, ErrorMessage = "length")]
    public string CategoryId {get; set;} = string.Empty;
    [Required(ErrorMessage = "required")]
    [StringLength(50,ErrorMessage= "maxlength")]
    public string Name {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public bool IsActive {get; set;} = true;
    public DateTime? UpdateAt {get; set;}
    public DateTime CreateAt {get; set;} = DateTime.Now;
    public ICollection<Ticket> Tickets {get; set;} = new List<Ticket>();
}