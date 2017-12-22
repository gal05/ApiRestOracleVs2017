using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiRestOracleVS2017.Datos;

namespace ApiRestOracleVS2017.Controllers
{
    public class USUARIOSController : ApiController
    {
        private UsuarioEntities db = new UsuarioEntities();

        // GET: api/USUARIOS
        public IQueryable<USUARIOS> GetUSUARIOS()
        {
            return db.USUARIOS;
        }

        // GET: api/USUARIOS/5
        [ResponseType(typeof(USUARIOS))]
        public async Task<IHttpActionResult> GetUSUARIOS(int id)
        {
            USUARIOS uSUARIOS = await db.USUARIOS.FindAsync(id);
            if (uSUARIOS == null)
            {
                return NotFound();
            }

            return Ok(uSUARIOS);
        }

        // PUT: api/USUARIOS/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSUARIOS(int id, USUARIOS uSUARIOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSUARIOS.ID)
            {
                return BadRequest();
            }

            db.Entry(uSUARIOS).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOSExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/USUARIOS
        [ResponseType(typeof(USUARIOS))]
        public async Task<IHttpActionResult> PostUSUARIOS(USUARIOS uSUARIOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USUARIOS.Add(uSUARIOS);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (USUARIOSExists(uSUARIOS.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = uSUARIOS.ID }, uSUARIOS);
        }

        // DELETE: api/USUARIOS/5
        [ResponseType(typeof(USUARIOS))]
        public async Task<IHttpActionResult> DeleteUSUARIOS(int id)
        {
            USUARIOS uSUARIOS = await db.USUARIOS.FindAsync(id);
            if (uSUARIOS == null)
            {
                return NotFound();
            }

            db.USUARIOS.Remove(uSUARIOS);
            await db.SaveChangesAsync();

            return Ok(uSUARIOS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USUARIOSExists(int id)
        {
            return db.USUARIOS.Count(e => e.ID == id) > 0;
        }
    }
}