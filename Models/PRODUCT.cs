using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class PRODUCT
    {

        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public int Id_Supplier { get; set; }
        [ForeignKey("Id_Supplier")]
        public virtual SUPPLIER SUPPLIER { get; set; }

        public int Id_Unit { get; set; }
        [ForeignKey("Id_Unit")]
        public virtual UNIT UNIT { get; set; }

        public int Id_Admin_Pae { get; set; }

        [ForeignKey("Id_Admin_Pae")]
        public virtual ADMIN_PAE ADMIN_PAE { get; set; }

        ////////////////////////////////////////

        public virtual ICollection<FOOD_PRODUCT> FOOD_PRODUCTS { get; set; }

    }
}
