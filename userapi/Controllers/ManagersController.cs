using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using userapi.Models;

namespace userapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly UsersContext _context;

        public ManagersController(UsersContext context)
        {
            _context = context;
        }

        // GET: api/Managers
        [HttpGet]
        public IEnumerable<Managers> GetManagers()
        {
            return _context.Managers.Include(m => m.Clients).Include(m => m.User);
        }

        // GET: api/Managers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetManagers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managers = await _context.Managers.FindAsync(id);

            if (managers == null)
            {
                return NotFound();
            }

            return Ok(managers);
        }


        // GET: api/Managers/GetClientsByName/peter
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetClientsByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var clients = await _context.Managers.Include(m => m.User).Include(m => m.Clients).Where(m => m.User.UserName == name).Select(m => m.Clients).ToListAsync();

            if (clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }

        // PUT: api/Managers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManagers([FromRoute] int id, [FromBody] Managers managers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != managers.UserId)
            {
                return BadRequest();
            }

            _context.Entry(managers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Managers
        [HttpPost]
        public async Task<IActionResult> PostManagers([FromBody] Managers managers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Managers.Add(managers);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ManagersExists(managers.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetManagers", new { id = managers.UserId }, managers);
        }

        // DELETE: api/Managers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManagers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managers = await _context.Managers.FindAsync(id);
            if (managers == null)
            {
                return NotFound();
            }

            _context.Managers.Remove(managers);
            await _context.SaveChangesAsync();

            return Ok(managers);
        }

        private bool ManagersExists(int id)
        {
            return _context.Managers.Any(e => e.UserId == id);
        }
    }
}