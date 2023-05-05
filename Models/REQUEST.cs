using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class REQUEST
    {

        [Key]
        public int ID { get; set; }

        [Required, Column(TypeName = "Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [Required, Column(TypeName = "Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Desired_Delivery_Date { get; set; }

        public int Id_Nutritionits_Ints { get; set; }
        [ForeignKey("Id_Nutritionits_Ints")]
        public virtual NUTRITIONITS_INTS NUTRITIONITS_INTS { get; set; }


        /////////////////////////////////
        public virtual ICollection<REQUEST_MENU> REQUEST_MENUS { get; set; }
        public virtual SHIPMENT SHIPMENT { get; set; }

    }
}
