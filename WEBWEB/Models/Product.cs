using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEBWEB.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public float Price { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }

}
