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

    [RoutePrefix("api/ProfileSMs")]
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

        // GET: api/ProfileEmails/5
        [HttpPost]
        [Route("GetProfileSM")]
        public async Task<IHttpActionResult> GetProfileSM(JObject form)
        {
            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.ProfileMSId;
            }
            catch
            {
                return BadRequest("Missing parameter.");
            }
            var profileMS = await GetProfileSMs().
                Where(u => u.ProfileMSId == id).FirstOrDefaultAsync();
            if (profileMS == null)
            {
                return NotFound();
            }

            return Ok(profileMS);
        }

        [HttpPost]
        [Route("GetProfileByNetWorkT")]
        public async Task<IHttpActionResult> GetProfileByNetWorkT(JObject form)
        {
            int id;
            int RedSocialId;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.UserId;
                RedSocialId = jsonObject.RedSocialId;
            }
            catch
            {
                return BadRequest("Missing parameter.");
            }
            var profileMS = await GetProfileSMs().
                Where(u => u.UserId == id && u.RedSocialId == RedSocialId).ToListAsync();
            if (profileMS == null)
            {
                return NotFound();
            }

            return Ok(profileMS);
        }

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

        // PUT: api/ProfileEmails/5
        //[ResponseType(typeof(void))]
        //[Route("PutProfileSM")]
        //public async Task<IHttpActionResult> PutProfileSM(ProfileEmail form)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    int id;
        //    dynamic jsonObject = form;
        //    try
        //    {
        //        id = jsonObject.ProfileMSId;
        //    }
        //    catch
        //    {
        //        return BadRequest("Missing parameter.");
        //    }


        //    db.Entry(form).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProfileSMExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    var profileSM = await GetProfileSMs().
        //       Where(u => u.ProfileMSId == id).FirstOrDefaultAsync();

        //    return Ok(profileSM);
        //}

        // PUT: api/ProfileSMs/5
        [ResponseType(typeof(void))]
        [Route("PutProfileSM")]
        public async Task<IHttpActionResult> PutProfileSM(ProfileSM form)
        {
            int id;
            dynamic jsonObject = form;
            try
            {
                id = jsonObject.ProfileMSId;
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
                if (!ProfileSMExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var profileSM= await GetProfileSMs().
               Where(u => u.ProfileMSId == id).FirstOrDefaultAsync();

            return Ok(profileSM);
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