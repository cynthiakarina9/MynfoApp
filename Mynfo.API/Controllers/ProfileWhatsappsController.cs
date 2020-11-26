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

    [RoutePrefix("api/ProfileWhatsapps")]
    public class ProfileWhatsappsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ProfileWhatsapps
        public IQueryable<ProfileWhatsapp> GetProfileWhatsapps()
        {
            return db.ProfileWhatsapps;
        }

        //// GET: api/ProfileWhatsapps/5
        //[ResponseType(typeof(ProfileWhatsapp))]
        //public async Task<IHttpActionResult> GetProfileWhatsapp(int id)
        //{
        //    ProfileWhatsapp profileWhatsapp = await db.ProfileWhatsapps.FindAsync(id);
        //    if (profileWhatsapp == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(profileWhatsapp);
        //}

        // POST: api/ProfileWhatsapps/5
        [HttpPost]
        [Route("GetProfileWhatsApp")]
        public async Task<IHttpActionResult> GetProfileWhatsApp(JObject form)
        {
            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.ProfileWhatsappId;
            }
            catch
            {
                return BadRequest("Missing parameter.");
            }
            var profileWhats = await GetProfileWhatsapps().
                Where(u => u.ProfileWhatsappId == id).FirstOrDefaultAsync();
            if (profileWhats == null)
            {
                return NotFound();
            }

            return Ok(profileWhats);
        }
        // GET: api/ProfileWhatsapps/5
        [ResponseType(typeof(ProfileWhatsapp))]
        public async Task<IHttpActionResult> GetProfileWhatsapp(int id)
        {
            var profileWhatsapp = await GetProfileWhatsapps().Where(u => u.UserId == id).ToListAsync();
            if (profileWhatsapp == null)
            {
                return NotFound();
            }

            return Ok(profileWhatsapp);
        }

        // PUT: api/ProfileEmails/
        [ResponseType(typeof(void))]
        [Route("PutProfileWhatsapps")]
        public async Task<IHttpActionResult> PutProfileWhatsapps(ProfileWhatsapp form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.ProfileWhatsappId;
            }
            catch
            {
                return BadRequest("Missing parameter.");
            }


            db.Entry(form).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileWhatsappExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var profileWhatsapp = await GetProfileWhatsapps().
               Where(u => u.ProfileWhatsappId == id).FirstOrDefaultAsync();

            return Ok(profileWhatsapp);
        }

        // PUT: api/ProfileWhatsapps/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfileWhatsapp(int id, ProfileWhatsapp profileWhatsapp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profileWhatsapp.ProfileWhatsappId)
            {
                return BadRequest();
            }

            db.Entry(profileWhatsapp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileWhatsappExists(id))
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

        // POST: api/ProfileWhatsapps
        [ResponseType(typeof(ProfileWhatsapp))]
        public async Task<IHttpActionResult> PostProfileWhatsapp(ProfileWhatsapp profileWhatsapp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfileWhatsapps.Add(profileWhatsapp);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = profileWhatsapp.ProfileWhatsappId }, profileWhatsapp);
        }

        // DELETE: api/ProfileWhatsapps/5
        [ResponseType(typeof(ProfileWhatsapp))]
        public async Task<IHttpActionResult> DeleteProfileWhatsapp(int id)
        {
            ProfileWhatsapp profileWhatsapp = await db.ProfileWhatsapps.FindAsync(id);
            if (profileWhatsapp == null)
            {
                return NotFound();
            }

            db.ProfileWhatsapps.Remove(profileWhatsapp);
            await db.SaveChangesAsync();

            return Ok(profileWhatsapp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfileWhatsappExists(int id)
        {
            return db.ProfileWhatsapps.Count(e => e.ProfileWhatsappId == id) > 0;
        }
    }
}