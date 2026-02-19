using API_FTN_V1._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_FTN_V1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotsController : ControllerBase
    {

        private readonly AppApiContext _context;

        public LotsController(AppApiContext context)
        {
            _context = context;
        }

        // créaction d'un Lot
        // POST : api/Lots/
        [HttpPost]
        public async Task<IActionResult> CreateLot([FromBody] Lot newLot)
        {            
            _context.Lots.Add(newLot);
            await _context.SaveChangesAsync();
            return Ok("LOT_CREATE_SUCCESSFULLY");
        }

        //Mise à jour d'un Lot
        //PUT : api/Lots/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLot(int id, [FromBody] Lot updatedLot)
        {
            var lot = await _context.Lots.FindAsync(id);

            if (lot == null)
                return NotFound("LOT_NOT_FOUND");

            lot.EntryDate = updatedLot.EntryDate;
            lot.Age = updatedLot.Age;
            lot.Phase = updatedLot.Phase;
            lot.Breed = updatedLot.Breed;
            lot.Status = updatedLot.Status;
            lot.InitialQuantity = updatedLot.InitialQuantity;
            lot.MortalityCount = updatedLot.MortalityCount;
            lot.CurrentQuantity =
                updatedLot.InitialQuantity - updatedLot.MortalityCount;

            await _context.SaveChangesAsync();
            return Ok("LOT_UPDATE_SUCCESSFULLY");
        }

        // supprimer un Lot
        // Delete : api/Lots/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLot(int id)
        {
            var lot = await _context.Lots.FindAsync(id);

            if (lot == null)
                return NotFound("LOT_NOT_FOUND");

            _context.Lots.Remove(lot);
            await _context.SaveChangesAsync();

            return Ok("LOT_DELETED_SUCCESSFULLY");
        }

        // recuperer un lot 
        // GET: api/Lots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lot>> GetLot(int id)
        {
            var lot = await _context.Lots.FindAsync(id);

            if (lot == null)
            {
                return NotFound();
            }

            return lot;
        }

        //pagination, envoi des donees en morceau au lieu de tous en une seul fois
        //GET api/lots?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetLots(int page = 1, int pageSize = 2)
        {
            if (pageSize > 50) pageSize = 50;
            if (pageSize <= 0) pageSize = 10;
            if (page <= 0) page = 1;


            var totalItems = await _context.Lots.CountAsync();

            var lots = await _context.Lots
                .AsNoTracking()  // Désactive le suivi des entités par EF Core (optimise les performances en lecture seule)
                .OrderBy(l => l.Id) // IMPORTANT pour pagination stable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

               
            return Ok(new 
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Data = lots
            });
        }






    }
}
