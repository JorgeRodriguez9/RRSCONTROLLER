using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class MENU
    {

        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public int Id_Category { get; set; }
        [ForeignKey("Id_Category")]
        public virtual CATEGORY CATEGORY { get; set; }

        public int Id_Nutritionits_Pae { get; set; }
        [ForeignKey("Id_Nutritionits_Pae")]
        public virtual NUTRITIONITS_PAE NUTRITIONITS_PAE { get; set; }

        ////////////////////////////////////////

        public virtual ICollection<MENU_FOOD> MENU_FOODS { get; set; }
        public virtual ICollection<REQUEST_MENU> REQUEST_MENUS { get; set; }

    }
}
