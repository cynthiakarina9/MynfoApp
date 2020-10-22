namespace Mynfo.API.Controllers
{
    using Domain;
    using Helpers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.Routing;

    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // : api/Users/ejemplo@ejemplo.com
        [HttpPost]
        [Authorize]
        [Route("GetUserByEmail")]
        public async Task<IHttpActionResult> GetUserByEmail(JObject form)
        {
            var email = string.Empty;
            dynamic jsonObject = form;
            try
            {
                email = jsonObject.Email.Value;
            }
            catch
            {
                return BadRequest("Missing parameter.");
            }

            var user = await db.Users.
                Where(u => u.Email.ToLower() == email.ToLower()).
                FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("LoginFacebook")]
        public async Task<IHttpActionResult> LoginFacebook(FacebookResponse profile)
        {
            try
            {
                var user = await db.Users.Where(u => u.Email == profile.Id).FirstOrDefaultAsync();
                if (user == null)
                {
                    user = new User
                    {
                        Email = profile.Id,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        ImagePath = profile.Picture.Data.Url,
                        UserTypeId = 2,
                    };

                    db.Users.Add(user);
                    UsersHelper.CreateUserASP(profile.Id, "User", profile.Id);
                }
                else
                {
                    user.FirstName = profile.FirstName;
                    user.LastName = profile.LastName;
                    user.ImagePath = profile.Picture.Data.Url;
                    db.Entry(user).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
                return Ok(true);
            }
            catch (DbEntityValidationException e)
            {
                var message = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    message = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("\n- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(JObject form)
        {
            var email = string.Empty;
            var currentPassword = string.Empty;
            var newPassword = string.Empty;
            dynamic jsonObject = form;

            try
            {
                email = jsonObject.Email.Value;
                currentPassword = jsonObject.CurrentPassword.Value;
                newPassword = jsonObject.NewPassword.Value;
            }
            catch
            {
                return BadRequest("Incorrect call");
            }

            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(email);

            if (userASP == null)
            {
                return BadRequest("Incorrect call");
            }

            var response = await userManager.ChangePasswordAsync(userASP.Id, currentPassword, newPassword);
            if (!response.Succeeded)
            {
                return BadRequest(response.Errors.FirstOrDefault());
            }

            return Ok("ok");
        }

        // PUT: api/Users/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (user.ImageArray != null && user.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(user.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    user.ImagePath = fullPath;
                }
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (user.ImageArray != null && user.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(user.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    user.ImagePath = fullPath;
                }
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();
            UsersHelper.CreateUserASP(user.Email, "User", user.Password);

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}