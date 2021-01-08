namespace Mynfo.API.Controllers
{
    using Mynfo.Domain;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [RoutePrefix("api/Box_ProfileSM")]
    public class Box_ProfileSMController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Box_ProfileSM
        public IQueryable<Box_ProfileSM> GetBox_ProfileSM()
        {
            return db.Box_ProfileSM;
        }

        //// GET: api/Box_ProfileSM/5
        //[ResponseType(typeof(Box_ProfileSM))]
        //public async Task<IHttpActionResult> GetBox_ProfileSM(int id)
        //{
        //    Box_ProfileSM box_ProfileSM = await db.Box_ProfileSM.FindAsync(id);
        //    if (box_ProfileSM == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(box_ProfileSM);
        //}

        // GET: api/Box_ProfileSM/GetBox_ProfileSM
        [HttpPost]
        [Route("GetBox_ProfileSM")]
        public async Task<IHttpActionResult> GetBox_ProfileSM(JObject form)
        {
            try
            {
                int idBox = 0;
                int idSM = 0;
                dynamic jsonObject = form;

                try
                {
                    idBox = jsonObject.BoxId;
                    idSM = jsonObject.ProfileMSId;
                }
                catch
                {
                    return BadRequest("Incorrect call.");
                }
                var box_ProfileSM = GetBox_ProfileSM().Where(u => u.BoxId == idBox && u.ProfileMSId == idSM);
                if (box_ProfileSM.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(box_ProfileSM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        // GET: api/Box_ProfileSM/GetBox_ProfileSM
        [Route("GetBox_ProfileSMId")]
        public IQueryable<Box_ProfileSM> GetBox_ProfileSMId(JObject form)
        {
            try
            {
                int idBox = 0;
                int idSM = 0;
                dynamic jsonObject = form;

                try
                {
                    idBox = jsonObject.BoxId;
                    idSM = jsonObject.ProfileMSId;
                }
                catch
                {
                    return null;
                }
                var box_ProfileSM = GetBox_ProfileSM().Where(u => u.BoxId == idBox && u.ProfileMSId == idSM);
                if (box_ProfileSM.Count() == 0)
                {
                    return null;
                }

                return box_ProfileSM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // PUT: api/Box_ProfileSM/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBox_ProfileSM(int id, Box_ProfileSM box_ProfileSM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != box_ProfileSM.Box_ProfileSMId)
            {
                return BadRequest();
            }

            db.Entry(box_ProfileSM).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Box_ProfileSMExists(id))
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

        // POST: api/Box_ProfileSM
        [ResponseType(typeof(Box_ProfileSM))]
        public async Task<IHttpActionResult> PostBox_ProfileSM(Box_ProfileSM box_ProfileSM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Box_ProfileSM.Add(box_ProfileSM);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = box_ProfileSM.Box_ProfileSMId }, box_ProfileSM);
        }

        // DELETE: api/Box_ProfileSM/5
        [ResponseType(typeof(Box_ProfileSM))]
        public async Task<IHttpActionResult> DeleteBox_ProfileSMRelations(JObject form)
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
                    id = jsonObject.ProfileMSId;
                }
                catch
                {
                    return BadRequest("Missing parameter.");
                }
                var box_ProfileSM = GetBox_ProfileSM().Where(u => u.ProfileMSId == id).ToList();
                if (box_ProfileSM == null)
                {
                    return NotFound();
                }

                db.Box_ProfileSM.RemoveRange(box_ProfileSM);
                await db.SaveChangesAsync();

                return Ok(box_ProfileSM);
            }
            catch (Exception)
            {

                throw;
            }

        }

        // DELETE: api/Box_ProfileSM/5
        [ResponseType(typeof(Box_ProfileSM))]
        public async Task<IHttpActionResult> DeleteBox_ProfileSM(int id)
        {
            var box_ProfileSM = GetBox_ProfileSM().Where(u => u.Box_ProfileSMId == id).FirstOrDefault();
            if (box_ProfileSM == null)
            {
                return NotFound();
            }

            db.Box_ProfileSM.Remove(box_ProfileSM);
            await db.SaveChangesAsync();

            return Ok(box_ProfileSM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Box_ProfileSMExists(int id)
        {
            return db.Box_ProfileSM.Count(e => e.Box_ProfileSMId == id) > 0;
        }
    }
}