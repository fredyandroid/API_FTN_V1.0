namespace API_FTN_V1._0.Models
{
    public class User
    {
       
        public int Id { get; set; } // Clé primaire
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }// Mot de passe (hashé)
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
       
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
