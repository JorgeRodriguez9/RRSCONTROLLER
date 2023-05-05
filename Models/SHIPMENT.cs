using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class SHIPMENT
    {

        [Key]
        public int ID { get; set; }

        [Required, Column(TypeName = "Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [StringLength(20)]
        public string Status { get; set; }
        public string Transport { get; set; }

        public int Id_Request { get; set; }
        [ForeignKey("Id_Request")]
        public virtual REQUEST REQUEST { get; set; }

        public int Id_Admin_Pae { get; set; }

        [ForeignKey("Id_Admin_Pae")]
        public virtual ADMIN_PAE ADMIN_PAE { get; set; }

        ////////////////////////////////////////////
        public virtual EVALUATION EVALUATION { get; set; }

    }
}
