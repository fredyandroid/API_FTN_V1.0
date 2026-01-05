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
                return BadRequest("L'email est déjà utilisé.");
            }

            // Vérifier si le nom d'utilisateur est déjà utilisé
            if (await _context.Users.AnyAsync(u => u.Username == newUser.Username))
            {
                return BadRequest("Le nom d'utilisateur est déjà utilisé.");
            }

            // Vérifier si le téléphone est déjà utilisé
            if (await _context.Users.AnyAsync(u => u.Phone == newUser.Phone))
            {
                return BadRequest("Le numéro de téléphone est déjà utilisé.");
            }

            // Hash du mot de passe
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            // Ajouter l'utilisateur à la base de données
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("Inscription réussie !");
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Vérifier l'utilisateur
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Identifier || u.Email == loginRequest.Identifier);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return Unauthorized("Non d'utilisateur ou mot de passe incorrect.");
            }
            
            if (!user.IsActive)
            {
                return Unauthorized("Le compte n'est pas activé. Veillez contacter l'entreprise pour activer votre compte");
            }
            
            return Ok("Connexion réussie !");
        }

        /*
         * Utilisez Unauthorized() lorsque l'authentification est requise mais que l'utilisateur n'a pas fourni de credentials valides (par exemple,
         * jeton d'authentification expiré ou absent).
           Utilisez BadRequest() lorsque les données envoyées dans la requête sont incorrectes ou manquantes, ou si le client a fait une erreur de format.
        */




    }
}
