using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Message")]
    public class Message:Notifies
    {

        [Column("id")]
        public int Id { get; set; }

        [Column("message_tittle")]
        [MaxLength(100)]
        public string MessageText { get; set; }

        [Column("ativo")]
        public bool Active { get; set; }

        [Column("creation_date")]
        public DateTime DateMessage { get; set; }

        [Column("alteration_date")]
        public DateTime DateAlteration { get; set; }


        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual AplicationUser ApplicationUser { get; set; }

    }
}
