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

    [RoutePrefix("api/Box_ProfileWhatsapp")]
    public class Box_ProfileWhatsappController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Box_ProfileWhatsapp
        public IQueryable<Box_ProfileWhatsapp> GetBox_ProfileWhatsapp()
        {
            return db.Box_ProfileWhatsapp;
        }

        //// GET: api/Box_ProfileWhatsapp/5
        //[ResponseType(typeof(Box_ProfileWhatsapp))]
        //public async Task<IHttpActionResult> GetBox_ProfileWhatsapp(int id)
        //{
        //    Box_ProfileWhatsapp box_ProfileWhatsapp = await db.Box_ProfileWhatsapp.FindAsync(id);
        //    if (box_ProfileWhatsapp == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(box_ProfileWhatsapp);
        //}

        // GET: api/Box_ProfilePhone/GetBox_ProfilePhone
        [HttpPost]
        [Route("GetBox_ProfileWhatsapp")]
        public async Task<IHttpActionResult> GetBox_ProfileWhatsapp(JObject form)
        {
            try
            {
                int idBox = 0;
                int idWhatsapp = 0;
                dynamic jsonObject = form;

                try
                {
                    idBox = jsonObject.BoxId;
                    idWhatsapp = jsonObject.ProfileWhatsappId;
                }
                catch
                {
                    return BadRequest("Incorrect call.");
                }
                var box_ProfileWhatsapp = GetBox_ProfileWhatsapp().Where(u => u.BoxId == idBox && u.ProfileWhatsappId == idWhatsapp);
                if (box_ProfileWhatsapp.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(box_ProfileWhatsapp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Box_ProfilePhone/GetBox_ProfilePhone
        [HttpPost]
        [Route("GetBox_ProfileWhatsappId")]
        public IQueryable<Box_ProfileWhatsapp> GetBox_ProfileWhatsappId(JObject form)
        {
            try
            {
                int idBox = 0;
                int idWhatsapp = 0;
                dynamic jsonObject = form;

                try
                {
                    idBox = jsonObject.BoxId;
                    idWhatsapp = jsonObject.ProfileWhatsappId;
                }
                catch
                {
                    return null;
                }
                var box_ProfileWhatsapp = GetBox_ProfileWhatsapp().Where(u => u.BoxId == idBox && u.ProfileWhatsappId == idWhatsapp);
                if (box_ProfileWhatsapp.Count() == 0)
                {
                    return null;
                }

                return box_ProfileWhatsapp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // PUT: api/Box_ProfileWhatsapp/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBox_ProfileWhatsapp(int id, Box_ProfileWhatsapp box_ProfileWhatsapp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != box_ProfileWhatsapp.Box_ProfileWhatsappId)
            {
                return BadRequest();
            }

            db.Entry(box_ProfileWhatsapp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Box_ProfileWhatsappExists(id))
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

        // POST: api/Box_ProfileWhatsapp
        [ResponseType(typeof(Box_ProfileWhatsapp))]
        public async Task<IHttpActionResult> PostBox_ProfileWhatsapp(Box_ProfileWhatsapp box_ProfileWhatsapp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Box_ProfileWhatsapp.Add(box_ProfileWhatsapp);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = box_ProfileWhatsapp.Box_ProfileWhatsappId }, box_ProfileWhatsapp);
        }

        // DELETE: api/Box_ProfileWhatsapp/5
        [ResponseType(typeof(Box_ProfileWhatsapp))]
        public async Task<IHttpActionResult> DeleteBox_ProfileWhatsapp(int id)
        {
            var box_ProfileWhatsapp = GetBox_ProfileWhatsapp().Where(u => u.Box_ProfileWhatsappId == id).FirstOrDefault();
            if (box_ProfileWhatsapp == null)
            {
                return NotFound();
            }

            db.Box_ProfileWhatsapp.Remove(box_ProfileWhatsapp);
            await db.SaveChangesAsync();

            return Ok(box_ProfileWhatsapp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Box_ProfileWhatsappExists(int id)
        {
            return db.Box_ProfileWhatsapp.Count(e => e.Box_ProfileWhatsappId == id) > 0;
        }
    }
}