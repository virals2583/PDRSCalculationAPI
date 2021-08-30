using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDRSCalculationAPI.Entity
{
    [Table("Procurement", Schema = "dbo")]
    public class Procurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Status")]        
        public string Status { get; set; }

        [Required]
        [Display(Name = "Progress")]
        public int Progress { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public double Amount { get; set; }
    }
}
