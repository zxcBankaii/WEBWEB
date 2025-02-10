namespace WEBWEB.Models;

public class OrderViewModel
{
    public int OrderId { get; set; }
    public string Status { get; set; }
    public DateTime DateCreate { get; set; }
    public List<Product> Products { get; set; } 
}
