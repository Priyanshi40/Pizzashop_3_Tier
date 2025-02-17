using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    // [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    // [Required(ErrorMessage = "Password is required")]
     public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool? Isactive { get; set; }

    public string? Address { get; set; }

    public int Zipcode { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public int? RoleId { get; set; }

    public int Createdby { get; set; }

    public DateTime Createdat { get; set; }

    public int Modifiedby { get; set; }

    public DateTime Modifiedat { get; set; }

    public RoleName Role { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Favouriteitem> Favouriteitems { get; } = new List<Favouriteitem>();

    public virtual Role? RoleNavigation { get; set; }

    public virtual State? State { get; set; }
}
