using Microsoft.EntityFrameworkCore;
using Project_ZF.Data;
using Project_ZF.ViewModels;

namespace Project_ZF.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnkostenController : ControllerBase
    {
        private readonly ZiekenFondsContext _context;

        public OnkostenController(ZiekenFondsContext context)
        {
            _context = context;
        }

        // GET: api/onkosten
        // postman - https://localhost:7106/api/onkosten
        [HttpGet]
        public async Task<IActionResult> GetAllOnkosten()
        {
            var onkosten = await _context.Onkosten.ToListAsync();
            return Ok(onkosten);
        }

        // GET: api/onkosten/{id}
        // postman - https://localhost:7106/api/onkosten/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOnkostenById(int id)
        {
            var onkosten = await _context.Onkosten.FindAsync(id);
            if (onkosten == null)
            {
                return NotFound();
            }
            return Ok(onkosten);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OnkostenViewModel>>> GetOnkosten()
        {
            var onkosten = await _context.Onkosten
                .Include(o => o.Groepsreis) // Navigatie naar Groepsreis
                .ThenInclude(g => g.Bestemming) // Navigatie naar Bestemming
                .Select(o => new OnkostenViewModel
                {
                    Id = o.Id,
                    BestemmingsNaam = o.Groepsreis.Bestemming.BestemmingsNaam, // BestemmingsNaam tonen
                    Titel = o.Titel,
                    Omschrijving = o.Omschrijving,
                    Bedrag = o.Bedrag,
                    Datum = o.Datum,
                    Foto = o.Foto
                })
                .ToListAsync();

            return Ok(onkosten);
        }




        /////////////////////////////////////////////////////////////////
        /// CreateOnkosten
        // https://localhost:7106/api/onkosten
        //
        //  {
        //  "GroepsreisId": 1,
        //  "Titel": "Vervoerskosten",
        //  "Omschrijving": "Busrit naar het park",
        //  "Bedrag": 35.50,
        //  "Datum": "2024-12-08",
        //  "Foto": "busrit.jpg"
        //  }
        /////////////////////////////////////////////////////////////////
        [HttpPost]
        public async Task<IActionResult> CreateOnkosten([FromBody] Onkosten onkosten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Controleer of de opgegeven GroepsreisId bestaat
            var groepsreis = await _context.Groepsreizen.FindAsync(onkosten.GroepsreisId);
            if (groepsreis == null)
            {
                return BadRequest(new { error = "Invalid GroepsreisId. The specified Groepsreis does not exist." });
            }

            _context.Onkosten.Add(onkosten);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOnkostenById), new { id = onkosten.Id }, onkosten);
        }


        // PUT: api/onkosten/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOnkosten(int id, [FromBody] Onkosten onkosten)
        {
            if (id != onkosten.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingOnkosten = await _context.Onkosten.FindAsync(id);
            if (existingOnkosten == null)
            {
                return NotFound();
            }

            // Update fields
            existingOnkosten.GroepsreisId = onkosten.GroepsreisId;
            existingOnkosten.Titel = onkosten.Titel;
            existingOnkosten.Omschrijving = onkosten.Omschrijving;
            existingOnkosten.Bedrag = onkosten.Bedrag;
            existingOnkosten.Datum = onkosten.Datum;
            existingOnkosten.Foto = onkosten.Foto;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/onkosten/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOnkosten(int id)
        {
            var onkosten = await _context.Onkosten.FindAsync(id);
            if (onkosten == null)
            {
                return NotFound();
            }

            _context.Onkosten.Remove(onkosten);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
 