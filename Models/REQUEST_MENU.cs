using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class REQUEST_MENU
    {

        [Key]
        public int ID { get; set; }
        public int Amount { get; set; }
        public int Id_Request { get; set; }
        [ForeignKey("Id_Request")]
        public virtual REQUEST REQUEST { get; set; }
        public int Id_Menu { get; set; }
        [ForeignKey("Id_Menu")]
        public virtual MENU MENU { get; set; }
       


    }
}
