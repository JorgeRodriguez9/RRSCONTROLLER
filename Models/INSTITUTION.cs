using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class INSTITUTION
    {

        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Adress { get; set; }

        public int Phone_Number { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int Id_Manager { get; set; }

        [ForeignKey("Id_Manager")]
        public virtual MANAGER_PAE MANAGER_PAE { get; set; }

        /////////////////////////////////

        public virtual ICollection<SECRETARY_INTS> SECRETARY_INTSs { get; set; }
        public virtual ICollection<NUTRITIONITS_INTS> NUTRITIONITS_INTSs { get; set; }

    }
}
