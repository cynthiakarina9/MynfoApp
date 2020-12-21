using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mynfo.Backend.Models;
using Mynfo.Domain;

namespace Mynfo.Backend.Controllers
{
    public class ProfileWhatsappsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ProfileWhatsapps
        public async Task<ActionResult> Index()
        {
            var profileWhatsapps = db.ProfileWhatsapps.Include(p => p.User);
            return View(await profileWhatsapps.ToListAsync());
        }

        // GET: ProfileWhatsapps/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileWhatsapp profileWhatsapp = await db.ProfileWhatsapps.FindAsync(id);
            if (profileWhatsapp == null)
            {
                return HttpNotFound();
            }
            return View(profileWhatsapp);
        }

        // GET: ProfileWhatsapps/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: ProfileWhatsapps/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileWhatsappId,Name,Number,UserId")] ProfileWhatsapp profileWhatsapp)
        {
            if (ModelState.IsValid)
            {
                db.ProfileWhatsapps.Add(profileWhatsapp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileWhatsapp.UserId);
            return View(profileWhatsapp);
        }

        // GET: ProfileWhatsapps/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileWhatsapp profileWhatsapp = await db.ProfileWhatsapps.FindAsync(id);
            if (profileWhatsapp == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileWhatsapp.UserId);
            return View(profileWhatsapp);
        }

        // POST: ProfileWhatsapps/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileWhatsappId,Name,Number,UserId")] ProfileWhatsapp profileWhatsapp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileWhatsapp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileWhatsapp.UserId);
            return View(profileWhatsapp);
        }

        // GET: ProfileWhatsapps/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileWhatsapp profileWhatsapp = await db.ProfileWhatsapps.FindAsync(id);
            if (profileWhatsapp == null)
            {
                return HttpNotFound();
            }
            return View(profileWhatsapp);
        }

        // POST: ProfileWhatsapps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProfileWhatsapp profileWhatsapp = await db.ProfileWhatsapps.FindAsync(id);
            db.ProfileWhatsapps.Remove(profileWhatsapp);
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
