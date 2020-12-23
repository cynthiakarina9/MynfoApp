namespace Mynfo.API.Controllers
{
    using Mynfo.Domain;
    using Newtonsoft.Json.Linq;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [RoutePrefix("api/ProfileEmails")]
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
        //public async Task<IHttpActionResult> GetP(int id)
        //{
        //    ProfileEmail profileEmail = await db.ProfileEmails.FindAsync(id);
        //    if (profileEmail == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(profileEmail);
        //}
        // GET: api/ProfileEmails/5
        [HttpPost]
        [Route("GetProfileEmail")]
        public async Task<IHttpActionResult> GetProfileEmail(JObject form)
        {
            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.ProfileEmailId;
            }
            catch
            {
                return BadRequest("Missing parameter.");
            }
            var profileEmail = await GetProfileEmails().
                Where(u => u.ProfileEmailId == id).FirstOrDefaultAsync();
            if (profileEmail == null)
            {
                return NotFound();
            }

            return Ok(profileEmail);
        }
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
        [Route("PutProfileEmail")]
        public async Task<IHttpActionResult> PutProfileEmail(ProfileEmail form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.ProfileEmailId;
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
                if (!ProfileEmailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var profileEmail = await GetProfileEmails().
               Where(u => u.ProfileEmailId == id).FirstOrDefaultAsync();

            return Ok(profileEmail);
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