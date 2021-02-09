using Mynfo.Backend.Models;
using Mynfo.Domain;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mynfo.Backend.Controllers
{
    public class ProfileSMsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ProfileSMs
        public async Task<ActionResult> Index()
        {
            var profileSMs = db.ProfileSMs.Include(p => p.RedSocial).Include(p => p.User);
            return View(await profileSMs.ToListAsync());
        }

        // GET: ProfileSMs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileSM profileSM = await db.ProfileSMs.FindAsync(id);
            if (profileSM == null)
            {
                return HttpNotFound();
            }
            return View(profileSM);
        }

        // GET: ProfileSMs/Create
        public ActionResult Create()
        {
            ViewBag.RedSocialId = new SelectList(db.RedSocials, "RedSocialId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: ProfileSMs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileMSId,link,ProfileName,UserId,RedSocialId")] ProfileSM profileSM)
        {
            if (ModelState.IsValid)
            {
                db.ProfileSMs.Add(profileSM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RedSocialId = new SelectList(db.RedSocials, "RedSocialId", "Name", profileSM.RedSocialId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileSM.UserId);
            return View(profileSM);
        }

        // GET: ProfileSMs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileSM profileSM = await db.ProfileSMs.FindAsync(id);
            if (profileSM == null)
            {
                return HttpNotFound();
            }
            ViewBag.RedSocialId = new SelectList(db.RedSocials, "RedSocialId", "Name", profileSM.RedSocialId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileSM.UserId);
            return View(profileSM);
        }

        // POST: ProfileSMs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileMSId,link,ProfileName,UserId,RedSocialId")] ProfileSM profileSM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileSM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RedSocialId = new SelectList(db.RedSocials, "RedSocialId", "Name", profileSM.RedSocialId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileSM.UserId);
            return View(profileSM);
        }

        // GET: ProfileSMs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileSM profileSM = await db.ProfileSMs.FindAsync(id);
            if (profileSM == null)
            {
                return HttpNotFound();
            }
            return View(profileSM);
        }

        // POST: ProfileSMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProfileSM profileSM = await db.ProfileSMs.FindAsync(id);
            db.ProfileSMs.Remove(profileSM);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
