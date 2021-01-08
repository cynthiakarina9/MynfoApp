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

    [RoutePrefix("api/Box_ProfilePhone")]
    public class Box_ProfilePhoneController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Box_ProfilePhone
        public IQueryable<Box_ProfilePhone> GetBox_ProfilePhone()
        {
            return db.Box_ProfilePhone;
        }

        //// GET: api/Box_ProfilePhone/5
        //[ResponseType(typeof(Box_ProfilePhone))]
        //public async Task<IHttpActionResult> GetBox_ProfilePhone(int id)
        //{
        //    Box_ProfilePhone box_ProfilePhone = await db.Box_ProfilePhone.FindAsync(id);
        //    if (box_ProfilePhone == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(box_ProfilePhone);
        //}

        // GET: api/Box_ProfilePhone/GetBox_ProfilePhone

        [HttpPost]
        [Route("GetBox_ProfilePhone")]
        public async Task<IHttpActionResult> GetBox_ProfilePhone(JObject form)
        {
            try
            {
                int idBox = 0;
                int idPhone = 0;
                dynamic jsonObject = form;

                try
                {
                    idBox = jsonObject.BoxId;
                    idPhone = jsonObject.ProfilePhoneId;
                }
                catch
                {
                    return BadRequest("Incorrect call.");
                }
                var box_ProfilePhone = GetBox_ProfilePhone().Where(u => u.BoxId == idBox && u.ProfilePhoneId == idPhone);
                if (box_ProfilePhone.Count() == 0 )
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
        [Route("GetBox_ProfilePhoneId")]
        public IQueryable<Box_ProfilePhone> GetBox_ProfilePhoneId(JObject form)
        {
            try
            {
                int idBox = 0;
                int idPhone = 0;
                dynamic jsonObject = form;

                try
                {
                    idBox = jsonObject.BoxId;
                    idPhone = jsonObject.ProfilePhoneId;
                }
                catch
                {
                    return null;
                }
                var box_ProfilePhone = db.Box_ProfilePhone.Where(u => u.BoxId == idBox && u.ProfilePhoneId == idPhone);
                if (box_ProfilePhone.Count() == 0)
                {
                    return null;
                }

                return box_ProfilePhone;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("GetBox_Relations")]
        public async Task<IHttpActionResult> GetBox_Relations(int boxid)
        {
            var box_ProfilePhone = db.Box_ProfilePhone.Where(u => u.BoxId == boxid).ToList();
            if (box_ProfilePhone == null)
            {
                return NotFound();
            }
            return Ok(box_ProfilePhone);
        }

        // PUT: api/Box_ProfilePhone/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBox_ProfilePhone(int id, Box_ProfilePhone box_ProfilePhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != box_ProfilePhone.Box_ProfilePhoneId)
            {
                return BadRequest();
            }

            db.Entry(box_ProfilePhone).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Box_ProfilePhoneExists(id))
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

        // POST: api/Box_ProfilePhone
        [ResponseType(typeof(Box_ProfilePhone))]
        public async Task<IHttpActionResult> PostBox_ProfilePhone(Box_ProfilePhone box_ProfilePhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Box_ProfilePhone.Add(box_ProfilePhone);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = box_ProfilePhone.Box_ProfilePhoneId }, box_ProfilePhone);
        }

        // DELETE: api/Box_ProfilePhone/5
        [ResponseType(typeof(Box_ProfilePhone))]
        public async Task<IHttpActionResult> DeleteBox_ProfilePhoneRelations(JObject form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int id;
                dynamic jsonObject = form;
                try
                {
                    id = jsonObject.ProfilePhoneId;
                }
                catch
                {
                    return BadRequest("Missing parameter.");
                }
                var box_ProfilePhone = GetBox_ProfilePhone().Where(u => u.ProfilePhoneId == id).ToList();
                if (box_ProfilePhone == null)
                {
                    return NotFound();
                }

                db.Box_ProfilePhone.RemoveRange(box_ProfilePhone);
                await db.SaveChangesAsync();

                return Ok(box_ProfilePhone);
            }
            catch (Exception)
            {

                throw;
            }

        }
        // DELETE: api/Box_ProfilePhone/5
        [ResponseType(typeof(Box_ProfilePhone))]
        public async Task<IHttpActionResult> DeleteBox_ProfilePhone(int id)
        {
            var box_ProfilePhone = GetBox_ProfilePhone().Where(u => u.Box_ProfilePhoneId == id).FirstOrDefault();
            if (box_ProfilePhone == null)
            {
                return NotFound();
            }

            db.Box_ProfilePhone.Remove(box_ProfilePhone);
            await db.SaveChangesAsync();

            return Ok(box_ProfilePhone);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Box_ProfilePhoneExists(int id)
        {
            return db.Box_ProfilePhone.Count(e => e.Box_ProfilePhoneId == id) > 0;
        }
    }
}