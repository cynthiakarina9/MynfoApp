namespace Mynfo.API.Controllers
{
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
    using Mynfo.Domain;
    using Newtonsoft.Json.Linq;

    [RoutePrefix("api/Box_ProfileEmail")]
    public class Box_ProfileEmailController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Box_ProfileEmail
        public IQueryable<Box_ProfileEmail> GetBox_ProfileEmail()
        {
            return db.Box_ProfileEmail;
        }

        //// GET: api/Box_ProfileEmail/5
        //[ResponseType(typeof(Box_ProfileEmail))]
        //public async Task<IHttpActionResult> GetBox_ProfileEmail(int id)
        //{
        //    Box_ProfileEmail box_ProfileEmail = await db.Box_ProfileEmail.FindAsync(id);
        //    if (box_ProfileEmail == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(box_ProfileEmail);
        //}

        // GET: api/Box_ProfileEmail/5
        [HttpPost]
        [Route("GetBox_ProfileEmail")]
        public async Task<IHttpActionResult> GetBox_ProfileEmail(JObject form)
        {
            try
            {
                int idBox = 0;
                int idEmail = 0;
                dynamic jsonObject = form;

                try
                {
                    idBox = jsonObject.BoxId;
                    idEmail = jsonObject.ProfileEmailId;
                }
                catch
                {
                    return BadRequest("Incorrect call.");
                }
                var box_ProfilePhone = GetBox_ProfileEmail().Where(u => u.BoxId == idBox && u.ProfileEmailId == idEmail);
                if (box_ProfilePhone.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(box_ProfilePhone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("GetBox_ProfileEmailId")]
        public IQueryable<Box_ProfileEmail> GetBox_ProfileEmailId(JObject form)
        {
            try
            {
                int idBox = 0;
                int idEmail = 0;
                dynamic jsonObject = form;

                try
                {
                    idBox = jsonObject.BoxId;
                    idEmail = jsonObject.ProfileEmailId;
                }
                catch
                {
                    return null;
                }
                var box_ProfileEmail = db.Box_ProfileEmail.Where(u => u.BoxId == idBox && u.ProfileEmailId == idEmail);
                if (box_ProfileEmail.Count() == 0)
                {
                    return null;
                }

                return box_ProfileEmail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // PUT: api/Box_ProfileEmail/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBox_ProfileEmail(int id, Box_ProfileEmail box_ProfileEmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != box_ProfileEmail.Box_ProfileEmailId)
            {
                return BadRequest();
            }

            db.Entry(box_ProfileEmail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Box_ProfileEmailExists(id))
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

        // POST: api/Box_ProfileEmail
        [ResponseType(typeof(Box_ProfileEmail))]
        public async Task<IHttpActionResult> PostBox_ProfileEmail(Box_ProfileEmail box_ProfileEmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Box_ProfileEmail.Add(box_ProfileEmail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = box_ProfileEmail.Box_ProfileEmailId }, box_ProfileEmail);
        }

        // DELETE: api/Box_ProfileEmail/5
        [ResponseType(typeof(Box_ProfileEmail))]
        public async Task<IHttpActionResult> DeleteBox_ProfileEmail(int id)
        {
            var box_ProfileEmail = GetBox_ProfileEmail().Where(u => u.Box_ProfileEmailId == id).FirstOrDefault();
            if (box_ProfileEmail == null)
            {
                return NotFound();
            }

            //db.Box_ProfileEmail.Remove(List<box_ProfileEmail>);
            db.Box_ProfileEmail.Remove(box_ProfileEmail);
            await db.SaveChangesAsync();

            return Ok(box_ProfileEmail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Box_ProfileEmailExists(int id)
        {
            return db.Box_ProfileEmail.Count(e => e.Box_ProfileEmailId == id) > 0;
        }
    }
}