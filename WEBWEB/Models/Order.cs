using System.ComponentModel.DataAnnotations.Schema;

namespace WEBWEB.Models;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public DateTime DateCreated { get; set; }

    public float TotalAmount { get; set; }

    public string Status { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
