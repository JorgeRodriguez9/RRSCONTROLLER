using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class SUPPLIER
    {

        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        /////////////////
        public virtual ICollection<PRODUCT> PRODUCTS { get; set; }

    }
}
