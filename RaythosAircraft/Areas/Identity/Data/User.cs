using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.AspNetCore.Identity;

namespace RaythosAircraft.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser    
{
    //there are default properties in this 'IdentityUser', here we are adding additional properties

    [Required]
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    [Required]
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Address { get; set; }

    [Required]
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Contact { get; set; }
}

