using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class ROLE
    {

        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Name_Role { get; set; }

        /////////////////
        public virtual ICollection<USER> USERS { get; set; }


    }
}
