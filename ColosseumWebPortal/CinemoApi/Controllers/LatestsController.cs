using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemoApi.Helpers;
using CinemoApi.Models;

namespace CinemoApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LatestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Latests
        public async Task<ActionResult> Index()
        {
            return View(await db.Latests.ToListAsync());
        }

        // GET: Latests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Latest latest = await db.Latests.FindAsync(id);
            if (latest == null)
            {
                return HttpNotFound();
            }
            return View(latest);
        }

        // GET: Latests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Latests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Latest latest)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/LatestMovies";

                if (latest.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(latest.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                latest.Logo = pic;
                db.Latests.Add(latest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(latest);
        }

        // GET: Latests/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Latest latest = await db.Latests.FindAsync(id);
            if (latest == null)
            {
                return HttpNotFound();
            }
            return View(latest);
        }

        // POST: Latests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Latest latest)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/LatestMovies";
                if (latest.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(latest.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    latest.Logo = pic;
                }
                db.Entry(latest).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(latest);
        }

        // GET: Latests/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Latest latest = await db.Latests.FindAsync(id);
            if (latest == null)
            {
                return HttpNotFound();
            }
            return View(latest);
        }

        // POST: Latests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Latest latest = await db.Latests.FindAsync(id);
            db.Latests.Remove(latest);
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
