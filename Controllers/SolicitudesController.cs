using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hogwarts.Models;

namespace Hogwarts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly SolicitudContext _context; //Contexto de la base de datos

        public SolicitudesController(SolicitudContext context)
        {
            _context = context; //Inyección de dependencia
        }

        // GET: api/Solicitudes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitud()
        {
            return await _context.Solicitud.ToListAsync(); //Retorna una lista de todas las solicitudes
        }

        // GET: api/Solicitudes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud>> GetSolicitud(long id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id); //Busca entre todas las solicitudes la que posea el id recibido

            if (solicitud == null)
            {
                return NotFound(); //Error 404 si no encuentra la solicitud
            }

            return solicitud;
        }

        // PUT: api/Solicitudes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitud(long id, Solicitud solicitud) //Para actualizar una solicitud se deben enviar todos los datos de la solicitud a editar
        {
            if (id != solicitud.Id)
            {
                return BadRequest(); //Si el id pasado por url difiere del id de la solicitud pasada por body arroja un error 415
            }

            _context.Entry(solicitud).State = EntityState.Modified; //Marca el estado de la entidad como Modified que indica que alguno de sus valores está siendo modificado

            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudExists(id))
                {
                    return NotFound(); //Si al intentar guardar la solicitud no se encuentra el id retorna 404
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Solicitudes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitud>> PostSolicitud(Solicitud solicitud)
        {
            _context.Solicitud.Add(solicitud); //Anade la solicitud al contexto de la db
            await _context.SaveChangesAsync();  //Guarda los cambios en la db

            return CreatedAtAction("GetSolicitud", new { id = solicitud.Id }, solicitud); //Retorna la solicitud creada similar a listCreateApiview en django
        }

        // DELETE: api/Solicitudes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud(long id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            _context.Solicitud.Remove(solicitud); //Remueve la solicitud del contexto de la db
            await _context.SaveChangesAsync(); 

            return NoContent();
        }

        private bool SolicitudExists(long id)
        {
            return _context.Solicitud.Any(e => e.Id == id); //Busca la solicitud que coincida con el id recibido
        }
    }
}
