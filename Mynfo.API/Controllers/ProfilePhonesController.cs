﻿using System;
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
    public class ProfilePhonesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ProfilePhones
        public IQueryable<ProfilePhone> GetProfilePhones()
        {
            return db.ProfilePhones;
        }

        // GET: api/ProfilePhones/5
        [ResponseType(typeof(ProfilePhone))]
        public async Task<IHttpActionResult> GetProfilePhone(int id)
        {
            ProfilePhone profilePhone = await db.ProfilePhones.FindAsync(id);
            if (profilePhone == null)
            {
                return NotFound();
            }

            return Ok(profilePhone);
        }

        // PUT: api/ProfilePhones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfilePhone(int id, ProfilePhone profilePhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profilePhone.ProfilePhoneId)
            {
                return BadRequest();
            }

            db.Entry(profilePhone).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilePhoneExists(id))
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

        // POST: api/ProfilePhones
        [ResponseType(typeof(ProfilePhone))]
        public async Task<IHttpActionResult> PostProfilePhone(ProfilePhone profilePhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfilePhones.Add(profilePhone);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = profilePhone.ProfilePhoneId }, profilePhone);
        }

        // DELETE: api/ProfilePhones/5
        [ResponseType(typeof(ProfilePhone))]
        public async Task<IHttpActionResult> DeleteProfilePhone(int id)
        {
            ProfilePhone profilePhone = await db.ProfilePhones.FindAsync(id);
            if (profilePhone == null)
            {
                return NotFound();
            }

            db.ProfilePhones.Remove(profilePhone);
            await db.SaveChangesAsync();

            return Ok(profilePhone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfilePhoneExists(int id)
        {
            return db.ProfilePhones.Count(e => e.ProfilePhoneId == id) > 0;
        }
    }
}