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
    public class Box_ProfilePhoneController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Box_ProfilePhone
        public async Task<ActionResult> Index()
        {
            var box_ProfilePhone = db.Box_ProfilePhone.Include(b => b.Box).Include(b => b.ProfilePhone);
            return View(await box_ProfilePhone.ToListAsync());
        }

        // GET: Box_ProfilePhone/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfilePhone box_ProfilePhone = await db.Box_ProfilePhone.FindAsync(id);
            if (box_ProfilePhone == null)
            {
                return HttpNotFound();
            }
            return View(box_ProfilePhone);
        }

        // GET: Box_ProfilePhone/Create
        public ActionResult Create()
        {
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name");
            ViewBag.ProfilePhoneId = new SelectList(db.ProfilePhones, "ProfilePhoneId", "Name");
            return View();
        }

        // POST: Box_ProfilePhone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Box_ProfilePhoneId,BoxId,ProfilePhoneId")] Box_ProfilePhone box_ProfilePhone)
        {
            if (ModelState.IsValid)
            {
                db.Box_ProfilePhone.Add(box_ProfilePhone);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfilePhone.BoxId);
            ViewBag.ProfilePhoneId = new SelectList(db.ProfilePhones, "ProfilePhoneId", "Name", box_ProfilePhone.ProfilePhoneId);
            return View(box_ProfilePhone);
        }

        // GET: Box_ProfilePhone/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfilePhone box_ProfilePhone = await db.Box_ProfilePhone.FindAsync(id);
            if (box_ProfilePhone == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfilePhone.BoxId);
            ViewBag.ProfilePhoneId = new SelectList(db.ProfilePhones, "ProfilePhoneId", "Name", box_ProfilePhone.ProfilePhoneId);
            return View(box_ProfilePhone);
        }

        // POST: Box_ProfilePhone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Box_ProfilePhoneId,BoxId,ProfilePhoneId")] Box_ProfilePhone box_ProfilePhone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(box_ProfilePhone).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfilePhone.BoxId);
            ViewBag.ProfilePhoneId = new SelectList(db.ProfilePhones, "ProfilePhoneId", "Name", box_ProfilePhone.ProfilePhoneId);
            return View(box_ProfilePhone);
        }

        // GET: Box_ProfilePhone/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfilePhone box_ProfilePhone = await db.Box_ProfilePhone.FindAsync(id);
            if (box_ProfilePhone == null)
            {
                return HttpNotFound();
            }
            return View(box_ProfilePhone);
        }

        // POST: Box_ProfilePhone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Box_ProfilePhone box_ProfilePhone = await db.Box_ProfilePhone.FindAsync(id);
            db.Box_ProfilePhone.Remove(box_ProfilePhone);
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
