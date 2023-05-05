using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class UNIT
    {

        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Name_Unit { get; set; }

        /////////////////
        public virtual ICollection<PRODUCT> PRODUCTS { get; set; }

    }
}
