using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRSCONTROLLER.Models
{
    public class FOOD
    {

        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public int Id_Nutritionits_Pae { get; set; }
        [ForeignKey("Id_Nutritionits_Pae")]
        public virtual NUTRITIONITS_PAE NUTRITIONITS_PAE { get; set; }

        ///////////////////////////////////
        public virtual ICollection<MENU_FOOD> MENU_FOODS { get; set; }

        public virtual ICollection<FOOD_PRODUCT> FOOD_PRODUCTS { get; set; }

    }
}
