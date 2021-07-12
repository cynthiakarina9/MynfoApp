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
    [Authorize(Roles = "Admin")]
    public class Box_ProfileSMController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Box_ProfileSM
        public async Task<ActionResult> Index()
        {
            var box_ProfileSM = db.Box_ProfileSM.Include(b => b.Box).Include(b => b.ProfileSM);
            return View(await box_ProfileSM.ToListAsync());
        }

        // GET: Box_ProfileSM/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileSM box_ProfileSM = await db.Box_ProfileSM.FindAsync(id);
            if (box_ProfileSM == null)
            {
                return HttpNotFound();
            }
            return View(box_ProfileSM);
        }

        // GET: Box_ProfileSM/Create
        public ActionResult Create()
        {
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name");
            ViewBag.ProfileMSId = new SelectList(db.ProfileSMs, "ProfileMSId", "link");
            return View();
        }

        // POST: Box_ProfileSM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Box_ProfileSMId,BoxId,ProfileMSId")] Box_ProfileSM box_ProfileSM)
        {
            if (ModelState.IsValid)
            {
                db.Box_ProfileSM.Add(box_ProfileSM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileSM.BoxId);
            ViewBag.ProfileMSId = new SelectList(db.ProfileSMs, "ProfileMSId", "link", box_ProfileSM.ProfileMSId);
            return View(box_ProfileSM);
        }

        // GET: Box_ProfileSM/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileSM box_ProfileSM = await db.Box_ProfileSM.FindAsync(id);
            if (box_ProfileSM == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileSM.BoxId);
            ViewBag.ProfileMSId = new SelectList(db.ProfileSMs, "ProfileMSId", "link", box_ProfileSM.ProfileMSId);
            return View(box_ProfileSM);
        }

        // POST: Box_ProfileSM/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Box_ProfileSMId,BoxId,ProfileMSId")] Box_ProfileSM box_ProfileSM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(box_ProfileSM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileSM.BoxId);
            ViewBag.ProfileMSId = new SelectList(db.ProfileSMs, "ProfileMSId", "link", box_ProfileSM.ProfileMSId);
            return View(box_ProfileSM);
        }

        // GET: Box_ProfileSM/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileSM box_ProfileSM = await db.Box_ProfileSM.FindAsync(id);
            if (box_ProfileSM == null)
            {
                return HttpNotFound();
            }
            return View(box_ProfileSM);
        }

        // POST: Box_ProfileSM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Box_ProfileSM box_ProfileSM = await db.Box_ProfileSM.FindAsync(id);
            db.Box_ProfileSM.Remove(box_ProfileSM);
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
