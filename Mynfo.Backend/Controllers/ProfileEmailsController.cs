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
    public class ProfileEmailsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ProfileEmails
        public async Task<ActionResult> Index()
        {
            var profileEmails = db.ProfileEmails.Include(p => p.User);
            return View(await profileEmails.ToListAsync());
        }

        // GET: ProfileEmails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileEmail profileEmail = await db.ProfileEmails.FindAsync(id);
            if (profileEmail == null)
            {
                return HttpNotFound();
            }
            return View(profileEmail);
        }

        // GET: ProfileEmails/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: ProfileEmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileEmailId,Name,Email,UserId")] ProfileEmail profileEmail)
        {
            if (ModelState.IsValid)
            {
                db.ProfileEmails.Add(profileEmail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileEmail.UserId);
            return View(profileEmail);
        }

        // GET: ProfileEmails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileEmail profileEmail = await db.ProfileEmails.FindAsync(id);
            if (profileEmail == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileEmail.UserId);
            return View(profileEmail);
        }

        // POST: ProfileEmails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileEmailId,Name,Email,UserId")] ProfileEmail profileEmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileEmail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", profileEmail.UserId);
            return View(profileEmail);
        }

        // GET: ProfileEmails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileEmail profileEmail = await db.ProfileEmails.FindAsync(id);
            if (profileEmail == null)
            {
                return HttpNotFound();
            }
            return View(profileEmail);
        }

        // POST: ProfileEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProfileEmail profileEmail = await db.ProfileEmails.FindAsync(id);
            db.ProfileEmails.Remove(profileEmail);
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
