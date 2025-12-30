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
        public string CountryName { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
       
        public bool IsActive { get; set; } = false; // Statut d'activation du compte
        /* apres avoir supprimer le dossier migration
         * PM> add-migration Initialcreate
         * PM> update-database
         * PM> Add-Migration AddEmailToUser  // pour ajouter une colonne "Email"
         * PM> update-database
         * PM> Add-Migration RemovePasswordFromUser // pour supprimer "PassWord"
         * PM> update-database


         * */

        


    }
}
