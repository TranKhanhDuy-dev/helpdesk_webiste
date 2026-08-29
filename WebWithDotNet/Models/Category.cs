namespace WebWithDotNet.Models;

public class Category
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public bool IsActive {get; set;} = true;
    public DateTime? UpdateAt {get; set;}
    public DateTime CreateAt {get; set;} = DateTime.Now;
    public ICollection<Ticket> Tickets {get; set;} = new List<Ticket>();
}