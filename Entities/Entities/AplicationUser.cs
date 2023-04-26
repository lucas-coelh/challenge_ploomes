using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class AplicationUser : IdentityUser
    {
        [Column("cpf")]
        public string CPF { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
        [Column("type")]
        public UserRole Type { get; set; }
    }
}
