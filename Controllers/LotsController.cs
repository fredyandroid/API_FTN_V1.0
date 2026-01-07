using API_FTN_V1._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(newLot);
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
            return Ok(lot);
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



    }
}
