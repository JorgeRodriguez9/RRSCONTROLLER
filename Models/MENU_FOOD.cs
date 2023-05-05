using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRSCONTROLLER.Models
{
    public class MENU_FOOD
    {
        [Key]
        public int ID { get; set; }
        public int Id_Menu { get; set; }
        [ForeignKey("Id_Menu")]
        public virtual MENU MENU { get; set; }

        public int Id_Food { get; set; }
        [ForeignKey("Id_Food")]
        public virtual FOOD FOOD { get; set; }
    }
}
