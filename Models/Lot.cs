using System.ComponentModel.DataAnnotations;
namespace API_FTN_V1._0.Models
{
    public class Lot
    {
        public int Id { get; set; } // Clé primaire
        [Required]
        public DateTime EntryDate { get; set; }
        public int Age { get; set; }
        [Required]
        public string Phase { get; set; }
        [Required]
        public string Breed { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int InitialQuantity { get; set; }
        [Required]
        public int CurrentQuantity { get; set; }
        [Required]
        public int MortalityCount { get; set; }
        [Required]
        public string LotCode { get; set; } 
        


    }
}
