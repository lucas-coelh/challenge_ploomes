using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("User_CPF")]
        public string CPF { get; set; }
        [Column("User_Role")]
        public UserRole? Role { get; set; }
    }
}
