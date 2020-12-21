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
    public class UserTagsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: UserTags
        public async Task<ActionResult> Index()
        {
            var userTags = db.UserTags.Include(u => u.User);
            return View(await userTags.ToListAsync());
        }

        // GET: UserTags/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTags userTags = await db.UserTags.FindAsync(id);
            if (userTags == null)
            {
                return HttpNotFound();
            }
            return View(userTags);
        }

        // GET: UserTags/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: UserTags/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserTagsId,UserId,TagId,Name")] UserTags userTags)
        {
            if (ModelState.IsValid)
            {
                db.UserTags.Add(userTags);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userTags.UserId);
            return View(userTags);
        }

        // GET: UserTags/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTags userTags = await db.UserTags.FindAsync(id);
            if (userTags == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userTags.UserId);
            return View(userTags);
        }

        // POST: UserTags/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserTagsId,UserId,TagId,Name")] UserTags userTags)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTags).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userTags.UserId);
            return View(userTags);
        }

        // GET: UserTags/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTags userTags = await db.UserTags.FindAsync(id);
            if (userTags == null)
            {
                return HttpNotFound();
            }
            return View(userTags);
        }

        // POST: UserTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserTags userTags = await db.UserTags.FindAsync(id);
            db.UserTags.Remove(userTags);
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
