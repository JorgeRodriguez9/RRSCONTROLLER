using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class MANAGER_PAE
    {

        [Key]
        public int ID { get; set; }

        public int Document { get; set; }

        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Last_Name { get; set; }

        [StringLength(30)]
        public string Adress { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        
        public int Id_User { get; set; }
        [ForeignKey("Id_User")]
        public virtual USER USER { get; set; }

        //////////////
        public virtual ICollection<INSTITUTION> INSTITUTIONS { get; set; }


    }
}
