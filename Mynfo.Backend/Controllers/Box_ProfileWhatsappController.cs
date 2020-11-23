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
    public class Box_ProfileWhatsappController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Box_ProfileWhatsapp
        public async Task<ActionResult> Index()
        {
            var box_ProfileWhatsapp = db.Box_ProfileWhatsapp.Include(b => b.Box);
            return View(await box_ProfileWhatsapp.ToListAsync());
        }

        // GET: Box_ProfileWhatsapp/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileWhatsapp box_ProfileWhatsapp = await db.Box_ProfileWhatsapp.FindAsync(id);
            if (box_ProfileWhatsapp == null)
            {
                return HttpNotFound();
            }
            return View(box_ProfileWhatsapp);
        }

        // GET: Box_ProfileWhatsapp/Create
        public ActionResult Create()
        {
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name");
            return View();
        }

        // POST: Box_ProfileWhatsapp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Box_ProfileWhatsappId,BoxId,ProfilePhoneId")] Box_ProfileWhatsapp box_ProfileWhatsapp)
        {
            if (ModelState.IsValid)
            {
                db.Box_ProfileWhatsapp.Add(box_ProfileWhatsapp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileWhatsapp.BoxId);
            return View(box_ProfileWhatsapp);
        }

        // GET: Box_ProfileWhatsapp/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileWhatsapp box_ProfileWhatsapp = await db.Box_ProfileWhatsapp.FindAsync(id);
            if (box_ProfileWhatsapp == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileWhatsapp.BoxId);
            return View(box_ProfileWhatsapp);
        }

        // POST: Box_ProfileWhatsapp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Box_ProfileWhatsappId,BoxId,ProfilePhoneId")] Box_ProfileWhatsapp box_ProfileWhatsapp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(box_ProfileWhatsapp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BoxId = new SelectList(db.Boxes, "BoxId", "Name", box_ProfileWhatsapp.BoxId);
            return View(box_ProfileWhatsapp);
        }

        // GET: Box_ProfileWhatsapp/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Box_ProfileWhatsapp box_ProfileWhatsapp = await db.Box_ProfileWhatsapp.FindAsync(id);
            if (box_ProfileWhatsapp == null)
            {
                return HttpNotFound();
            }
            return View(box_ProfileWhatsapp);
        }

        // POST: Box_ProfileWhatsapp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Box_ProfileWhatsapp box_ProfileWhatsapp = await db.Box_ProfileWhatsapp.FindAsync(id);
            db.Box_ProfileWhatsapp.Remove(box_ProfileWhatsapp);
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
