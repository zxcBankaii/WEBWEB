using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEBWEB.Models;

public class PaymentMethod
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Type { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string Details { get; set; }
}
