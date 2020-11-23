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
    public class ProfileEmailsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ProfileEmails
        public IQueryable<ProfileEmail> GetProfileEmails()
        {
            return db.ProfileEmails;
        }

        //// GET: api/ProfileEmails/5
        //[ResponseType(typeof(ProfileEmail))]
        //public async Task<IHttpActionResult> GetProfileEmail(int id)
        //{
        //    ProfileEmail profileEmail = await db.ProfileEmails.FindAsync(id);
        //    if (profileEmail == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(profileEmail);
        //}
        // GET: api/ProfileEmails/5
        [ResponseType(typeof(ProfileEmail))]
        public async Task<IHttpActionResult> GetProfileEmailByUser(int id)
        {
            var profileEmail = await GetProfileEmails().Where(u => u.UserId == id).ToListAsync();
            if (profileEmail == null)
            {
                return NotFound();
            }

            return Ok(profileEmail);
        }

        // PUT: api/ProfileEmails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfileEmail(int id, ProfileEmail profileEmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profileEmail.ProfileEmailId)
            {
                return BadRequest();
            }

            db.Entry(profileEmail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileEmailExists(id))
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

        // POST: api/ProfileEmails
        [ResponseType(typeof(ProfileEmail))]
        public async Task<IHttpActionResult> PostProfileEmail(ProfileEmail profileEmail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfileEmails.Add(profileEmail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = profileEmail.ProfileEmailId }, profileEmail);
        }

        // DELETE: api/ProfileEmails/5
        [ResponseType(typeof(ProfileEmail))]
        public async Task<IHttpActionResult> DeleteProfileEmail(int id)
        {
            ProfileEmail profileEmail = await db.ProfileEmails.FindAsync(id);
            if (profileEmail == null)
            {
                return NotFound();
            }

            db.ProfileEmails.Remove(profileEmail);
            await db.SaveChangesAsync();

            return Ok(profileEmail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfileEmailExists(int id)
        {
            return db.ProfileEmails.Count(e => e.ProfileEmailId == id) > 0;
        }
    }
}