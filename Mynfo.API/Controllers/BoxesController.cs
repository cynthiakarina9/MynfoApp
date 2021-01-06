﻿namespace Mynfo.API.Controllers
{
    using Mynfo.Domain;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [RoutePrefix("api/Boxes")]
    public class BoxesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Boxes
        public IQueryable<Box> GetBoxes()
        {
            return db.Boxes;
        }

        // GET: api/Boxes/5
        [ResponseType(typeof(Box))]
        public async Task<IHttpActionResult> GetBox(int id)
        {
            Box box = await db.Boxes.FindAsync(id);
            if (box == null)
            {
                return NotFound();
            }

            return Ok(box);
        }

        //// PUT: api/Boxes/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutBox(int id, Box box)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != box.BoxId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(box).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BoxExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // PUT: api/Boxes/PutBox1/5
        [ResponseType(typeof(Box))]
        public async Task<Box> PutBox1(int id, Box box)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            if (id != box.BoxId)
            {
                return null;
            }

            db.Entry(box).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoxExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return box;
        }

        // POST: api/Boxes
        [ResponseType(typeof(Box))]
        public async Task<IHttpActionResult> PostBox(Box box)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Boxes.Add(box);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = box.BoxId }, box);
        }

        // DELETE: api/Boxes/5
        [ResponseType(typeof(Box))]
        public async Task<IHttpActionResult> DeleteBox(int id)
        {
            Box box = await db.Boxes.FindAsync(id);
            if (box == null)
            {
                return NotFound();
            }

            db.Boxes.Remove(box);
            await db.SaveChangesAsync();

            return Ok(box);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BoxExists(int id)
        {
            return db.Boxes.Count(e => e.BoxId == id) > 0;
        }
    }
}