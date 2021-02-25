namespace Mynfo.API.Controllers
{
    using Mynfo.Domain;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
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

        // GET: api/Boxes/GetBoxDefault/1
        [HttpPost]
        [Route("GetBoxDefault")]
        [ResponseType(typeof(Box))]
        public async Task<Box> GetBoxDefault(JObject form)
        {
            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.UserId;
            }
            catch
            {
                return null;
            }
            Box box = GetBoxes().Where(u => u.UserId == id && u.BoxDefault == true).FirstOrDefault();
            if (box == null)
            {
                return null;
            }

            return box;
        }

        // GET: api/Boxes/GetBoxNoDefault/1
        [HttpPost]
        [Route("GetBoxNoDefault")]
        [ResponseType(typeof(Box))]
        public async Task<List<Box>> GetBoxNoDefault(JObject form)
        {
            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.UserId;
            }
            catch
            {
                return null;
            }
            var box = GetBoxes().Where(u => u.UserId == id && u.BoxDefault == false).ToList();
            if (box.Count == 0)
            {
                return null;
            }

            return box;
        }

        // GET: api/Boxes/GetLastBox/1
        [HttpPost]
        [Route("GetLastBox")]
        [ResponseType(typeof(Box))]
        public async Task<Box> GetLastBox(JObject form)
        {
            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.UserId;
            }
            catch
            {
                return null;
            }
            var boxList = GetBoxes().Where(u => u.UserId == id).ToList();
            var box2 = boxList.Max(u => u.BoxId);
            var box = GetBoxes().Where(u => u.BoxId == box2).FirstOrDefault();
            if (box == null)
            {
                return null;
            }

            return box;
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
        //GET: api/LastBox

        // PUT: api/Boxes/PutBox1/5
        [ResponseType(typeof(Box))]
        public async Task<Box> PutBox(int id, Box box)
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
        
        //// PUT: api/Boxes/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutBoxDefault(JObject form)
        //{
        //    try
        //    {
        //        int idBox = 0;
        //        bool def = false;
        //        dynamic jsonObject = form;

        //        try
        //        {
        //            idBox = jsonObject.BoxId;
        //            def = jsonObject.BoxDefault;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //        var boxdefault = GetBoxes().Where(u => u.BoxId == idBox).FirstOrDefault();
        //        if (boxdefault != null)
        //        {
        //            def = boxdefault.BoxDefault;
        //            db.Entry(boxdefault.BoxDefault = def).State = EntityState.Modified;
        //        }
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BoxExists(idBox))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return Ok(boxdefault);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

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