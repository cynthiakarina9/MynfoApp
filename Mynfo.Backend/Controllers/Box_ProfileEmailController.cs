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
    public class Box_ProfileEmailController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Box_ProfileEmail
        public async Task<ActionResult> Index()
        {
            var box_ProfileEmail = db.Box_ProfileEmail.Include(b => b.Box).Include(b => b.ProfileEmail);
            return View(await box_ProfileEmail.ToListAsync());
        }

        // GET: Box_ProfileEmail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileEmail box_ProfileEmail = await db.Box_ProfileEmail.FindAsync(id);
            if (box_ProfileEmail == null)
            {
                return HttpNotFound();
            }
            return View(box_ProfileEmail);
        }

        // GET: Box_ProfileEmail/Create
        public ActionResult Create()
        {
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name");
            ViewBag.ProfileEmailId = new SelectList(db.ProfileEmails, "ProfileEmailId", "Name");
            return View();
        }

        // POST: Box_ProfileEmail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Box_ProfileEmailId,BoxId,ProfileEmailId")] Box_ProfileEmail box_ProfileEmail)
        {
            if (ModelState.IsValid)
            {
                db.Box_ProfileEmail.Add(box_ProfileEmail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileEmail.BoxId);
            ViewBag.ProfileEmailId = new SelectList(db.ProfileEmails, "ProfileEmailId", "Name", box_ProfileEmail.ProfileEmailId);
            return View(box_ProfileEmail);
        }

        // GET: Box_ProfileEmail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileEmail box_ProfileEmail = await db.Box_ProfileEmail.FindAsync(id);
            if (box_ProfileEmail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileEmail.BoxId);
            ViewBag.ProfileEmailId = new SelectList(db.ProfileEmails, "ProfileEmailId", "Name", box_ProfileEmail.ProfileEmailId);
            return View(box_ProfileEmail);
        }

        // POST: Box_ProfileEmail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Box_ProfileEmailId,BoxId,ProfileEmailId")] Box_ProfileEmail box_ProfileEmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(box_ProfileEmail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileEmail.BoxId);
            ViewBag.ProfileEmailId = new SelectList(db.ProfileEmails, "ProfileEmailId", "Name", box_ProfileEmail.ProfileEmailId);
            return View(box_ProfileEmail);
        }

        // GET: Box_ProfileEmail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileEmail box_ProfileEmail = await db.Box_ProfileEmail.FindAsync(id);
            if (box_ProfileEmail == null)
            {
                return HttpNotFound();
            }
            return View(box_ProfileEmail);
        }

        // POST: Box_ProfileEmail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Box_ProfileEmail box_ProfileEmail = await db.Box_ProfileEmail.FindAsync(id);
            db.Box_ProfileEmail.Remove(box_ProfileEmail);
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
