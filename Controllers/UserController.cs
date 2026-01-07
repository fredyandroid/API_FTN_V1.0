using API_FTN_V1._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_FTN_V1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {



        private readonly UserApiContext _context;

        public UserController(UserApiContext context)
        {
            _context = context;
        }

        // POST: api/User/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Models.User newUser)
        {
            // Vérifier si l'email est déjà utilisé
            if (await _context.Users.AnyAsync(u => u.Email == newUser.Email))
            {
                //L'email est déjà utilisé.
                return BadRequest("EMAIL_ALREADY_EXISTS");
            }

            // Vérifier si le nom d'utilisateur est déjà utilisé
            if (await _context.Users.AnyAsync(u => u.Username == newUser.Username))
            {
                //Le nom d'utilisateur est déjà utilisé.
                return BadRequest("USERNAME_ALREADY_EXISTS");
            }

            // Vérifier si le téléphone est déjà utilisé
            if (await _context.Users.AnyAsync(u => u.Phone == newUser.Phone))
            {
                //Le numéro de téléphone est déjà utilisé.
                return BadRequest("PHONE_ALREADY_EXISTS");
            }

            // Hash du mot de passe
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            // Ajouter l'utilisateur à la base de données
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            //Inscription réussie !
            return Ok("REGISTER_SUCCESS");
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Vérifier l'utilisateur
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Identifier || u.Email == loginRequest.Identifier);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                //Non d'utilisateur ou mot de passe incorrect.
                return Unauthorized("INVALID_CREDENTIALS");                
            }         

            if (!user.IsActive)
            {
                //Le compte n'est pas activé. Veillez contacter l'entreprise pour activer votre compte
                return Unauthorized("ACCOUNT_NOT_ACTIVE");
            }
            //Connexion réussie !
            return Ok("LOGIN_SUCCESS"); 
        }

        /*
         * Utilisez Unauthorized() lorsque l'authentification est requise mais que l'utilisateur n'a pas fourni de credentials valides (par exemple,
         * jeton d'authentification expiré ou absent).
           Utilisez BadRequest() lorsque les données envoyées dans la requête sont incorrectes ou manquantes, ou si le client a fait une erreur de format.
        */




    }
}
