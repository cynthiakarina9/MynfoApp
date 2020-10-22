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
    public class RedSocialsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: RedSocials
        public async Task<ActionResult> Index()
        {
            return View(await db.RedSocials.ToListAsync());
        }

        // GET: RedSocials/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RedSocial redSocial = await db.RedSocials.FindAsync(id);
            if (redSocial == null)
            {
                return HttpNotFound();
            }
            return View(redSocial);
        }

        // GET: RedSocials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RedSocials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RedSocialId,Name")] RedSocial redSocial)
        {
            if (ModelState.IsValid)
            {
                db.RedSocials.Add(redSocial);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(redSocial);
        }

        // GET: RedSocials/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RedSocial redSocial = await db.RedSocials.FindAsync(id);
            if (redSocial == null)
            {
                return HttpNotFound();
            }
            return View(redSocial);
        }

        // POST: RedSocials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RedSocialId,Name")] RedSocial redSocial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(redSocial).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(redSocial);
        }

        // GET: RedSocials/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RedSocial redSocial = await db.RedSocials.FindAsync(id);
            if (redSocial == null)
            {
                return HttpNotFound();
            }
            return View(redSocial);
        }

        // POST: RedSocials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RedSocial redSocial = await db.RedSocials.FindAsync(id);
            db.RedSocials.Remove(redSocial);
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
