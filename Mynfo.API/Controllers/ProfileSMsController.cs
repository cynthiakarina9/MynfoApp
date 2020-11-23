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

namespace Mynfo.API.Controllers
{
    public class ProfileSMsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ProfileSMs
        public IQueryable<ProfileSM> GetProfileSMs()
        {
            return db.ProfileSMs;
        }

        // GET: api/ProfileSMs/5
        //[ResponseType(typeof(ProfileSM))]
        //public async Task<IHttpActionResult> GetProfileSM(int id)
        //{
        //    ProfileSM profileSM = await db.ProfileSMs.FindAsync(id);
        //    if (profileSM == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(profileSM);
        //}
        // GET: api/ProfileSMs/5
        [ResponseType(typeof(ProfileSM))]
        public async Task<IHttpActionResult> GetProfileSMByUser(int id)
        {
            
            var profileSM = await  GetProfileSMs().Where(u => u.UserId == id).ToListAsync();
            if (profileSM == null)
            {
                return NotFound();
            }

            return Ok(profileSM);
        }

        // PUT: api/ProfileSMs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfileSM(int id, ProfileSM profileSM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profileSM.ProfileMSId)
            {
                return BadRequest();
            }

            db.Entry(profileSM).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileSMExists(id))
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

        // POST: api/ProfileSMs
        [ResponseType(typeof(ProfileSM))]
        public async Task<IHttpActionResult> PostProfileSM(ProfileSM profileSM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfileSMs.Add(profileSM);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = profileSM.ProfileMSId }, profileSM);
        }

        // DELETE: api/ProfileSMs/5
        [ResponseType(typeof(ProfileSM))]
        public async Task<IHttpActionResult> DeleteProfileSM(int id)
        {
            ProfileSM profileSM = await db.ProfileSMs.FindAsync(id);
            if (profileSM == null)
            {
                return NotFound();
            }

            db.ProfileSMs.Remove(profileSM);
            await db.SaveChangesAsync();

            return Ok(profileSM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfileSMExists(int id)
        {
            return db.ProfileSMs.Count(e => e.ProfileMSId == id) > 0;
        }
    }
}