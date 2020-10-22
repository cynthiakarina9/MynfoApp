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
    public class ProfilePhonesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ProfilePhones
        public async Task<ActionResult> Index()
        {
            var profilePhones = db.ProfilePhones.Include(p => p.User);
            return View(await profilePhones.ToListAsync());
        }

        // GET: ProfilePhones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfilePhone profilePhone = await db.ProfilePhones.FindAsync(id);
            if (profilePhone == null)
            {
                return HttpNotFound();
            }
            return View(profilePhone);
        }

        // GET: ProfilePhones/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: ProfilePhones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfilePhoneId,Name,Number,UserId")] ProfilePhone profilePhone)
        {
            if (ModelState.IsValid)
            {
                db.ProfilePhones.Add(profilePhone);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profilePhone.UserId);
            return View(profilePhone);
        }

        // GET: ProfilePhones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfilePhone profilePhone = await db.ProfilePhones.FindAsync(id);
            if (profilePhone == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profilePhone.UserId);
            return View(profilePhone);
        }

        // POST: ProfilePhones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfilePhoneId,Name,Number,UserId")] ProfilePhone profilePhone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profilePhone).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profilePhone.UserId);
            return View(profilePhone);
        }

        // GET: ProfilePhones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfilePhone profilePhone = await db.ProfilePhones.FindAsync(id);
            if (profilePhone == null)
            {
                return HttpNotFound();
            }
            return View(profilePhone);
        }

        // POST: ProfilePhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProfilePhone profilePhone = await db.ProfilePhones.FindAsync(id);
            db.ProfilePhones.Remove(profilePhone);
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
