namespace WebWithDotNet.Models;

using WebWithDotNet.Models.Enums;

public class Ticket
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string RequestDetail {get; set;} = string.Empty;
    public TicketStatus Status {get; set;} = TicketStatus.Open;
    public TicketPriority Priority {get; set;} = TicketPriority.Low;
    public int CategoryId {get; set;}
    public Category Category {get; set;} = null!;
    public string CreatedBy {get; set;} = string.Empty;
    public DateTime? UpdateAt {get; set;}
    public DateTime CreateAt {get; set;} = DateTime.Now;
}