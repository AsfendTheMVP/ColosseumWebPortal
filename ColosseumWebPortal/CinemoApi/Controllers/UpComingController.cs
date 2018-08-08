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
    public class UpComingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UpComing
        public async Task<ActionResult> Index()
        {
            return View(await db.UpComingMovies.ToListAsync());
        }

        // GET: UpComing/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpComingMovies upComingMovies = await db.UpComingMovies.FindAsync(id);
            if (upComingMovies == null)
            {
                return HttpNotFound();
            }
            return View(upComingMovies);
        }

        // GET: UpComing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UpComing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UpComingMovies upComingMovies)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/UpComingMovies";

                if (upComingMovies.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(upComingMovies.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                upComingMovies.Logo = pic;
                db.UpComingMovies.Add(upComingMovies);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(upComingMovies);
        }

        // GET: UpComing/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpComingMovies upComingMovies = await db.UpComingMovies.FindAsync(id);
            if (upComingMovies == null)
            {
                return HttpNotFound();
            }
            return View(upComingMovies);
        }

        // POST: UpComing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpComingMovies upComingMovies)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/UpComingMovies";
                if (upComingMovies.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(upComingMovies.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    upComingMovies.Logo = pic;
                }
                db.Entry(upComingMovies).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(upComingMovies);
        }

        // GET: UpComing/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpComingMovies upComingMovies = await db.UpComingMovies.FindAsync(id);
            if (upComingMovies == null)
            {
                return HttpNotFound();
            }
            return View(upComingMovies);
        }

        // POST: UpComing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UpComingMovies upComingMovies = await db.UpComingMovies.FindAsync(id);
            db.UpComingMovies.Remove(upComingMovies);
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
