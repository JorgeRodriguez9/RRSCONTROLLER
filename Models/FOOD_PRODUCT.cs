using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class FOOD_PRODUCT
    {

        [Key]
        public int ID { get; set; }

        public int Amount { get; set; }

        public int Id_Food { get; set; }
        [ForeignKey("Id_Food")]
        public virtual FOOD FOOD { get; set; }
        public int Id_Product { get; set; }
        [ForeignKey("Id_Product")]
        public virtual PRODUCT PRODUCT { get; set; }

        
    }
}
