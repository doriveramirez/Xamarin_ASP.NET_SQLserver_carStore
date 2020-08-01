using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MVCCrudAPI.Models;

namespace MVCCrudAPI.Controllers
{
    public class FuncionesController : ApiController
    {
        private ValperEntities db = new ValperEntities();

        // GET: api/Funciones
        public IQueryable<Funciones> GetFunciones()
        {
            return db.Funciones;
        }

        // GET: api/Funciones/5
        [ResponseType(typeof(Funciones))]
        public IHttpActionResult GetFunciones(string id)
        {
            Funciones funciones = db.Funciones.Find(id);
            if (funciones == null)
            {
                return NotFound();
            }

            return Ok(funciones);
        }

        // PUT: api/Funciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFunciones(string id, Funciones funciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != funciones.Id)
            {
                return BadRequest();
            }

            db.Entry(funciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionesExists(id))
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

        // POST: api/Funciones
        [ResponseType(typeof(Funciones))]
        public IHttpActionResult PostFunciones(Funciones funciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Funciones.Add(funciones);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FuncionesExists(funciones.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = funciones.Id }, funciones);
        }

        // DELETE: api/Funciones/5
        [ResponseType(typeof(Funciones))]
        public IHttpActionResult DeleteFunciones(string id)
        {
            Funciones funciones = db.Funciones.Find(id);
            if (funciones == null)
            {
                return NotFound();
            }

            db.Funciones.Remove(funciones);
            db.SaveChanges();

            return Ok(funciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuncionesExists(string id)
        {
            return db.Funciones.Count(e => e.Id == id) > 0;
        }
    }
}