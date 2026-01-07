using System.ComponentModel.DataAnnotations;

namespace API_FTN_V1._0.Models
{
    public class User
    {
       
        public int Id { get; set; } // Clé primaire
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }// Mot de passe (hashé)
        [Required]
        public int Age { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]

        public bool IsActive { get; set; } = false; // Statut d'activation du compte
   
        /* apres avoir supprimer le dossier migration
         * PM> add-migration Initialcreate
         * PM> update-database
         * PM> Add-Migration AddEmailToUser  // mise à jour de la base de donnée
         * PM> update-database
         * PM> Add-Migration RemovePasswordFromUser // mise à jour de la base de donnée
         * PM> update-database


         * */





    }
}
