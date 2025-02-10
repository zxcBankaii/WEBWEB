using System.ComponentModel.DataAnnotations;

namespace WEBWEB.Models;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; }

    public bool IsAdmin { get; set; }
}
