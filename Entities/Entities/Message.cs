using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("TB_Message")]
    public class Message : Notifies
    {

        [Column("Msg_Id")]
        public int Id { get; set; }

        [Column("Msg_Title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Column("Msg_Active")]
        public bool Active { get; set; }

        [Column("Msg_Created_Date")]
        public DateTime CreatedDate { get; set; }

        [Column("Msg_Modified_Date")]
        public DateTime ModifiedDate { get; set; }


        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
