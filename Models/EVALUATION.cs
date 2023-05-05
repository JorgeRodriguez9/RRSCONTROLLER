using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RRSCONTROLLER.Models
{
    public class EVALUATION
    {

        [Key]
        public int ID { get; set; }

        [StringLength(5)]
        public string Received { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(5)]
        public string Correct_Quantity { get; set; }

        [Required, Column(TypeName = "Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date_Received { get; set; }

        public int Id_Shipment { get; set; }
        [ForeignKey("Id_Shipment")]
        public virtual SHIPMENT SHIPMENT { get; set; }

        public int Id_Secretary_Inst { get; set; }
        [ForeignKey("Id_Secretary_Inst")]
        public virtual SECRETARY_INTS SECRETARY_INTS { get; set; }

    }
}
