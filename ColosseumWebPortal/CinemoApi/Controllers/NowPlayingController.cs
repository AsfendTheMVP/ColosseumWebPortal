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
    public class NowPlayingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NowPlayingAdmin
        public async Task<ActionResult> Index()
        {
            return View(await db.NowPlayingMovies.ToListAsync());
        }

        // GET: NowPlayingAdmin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NowPlayingMovies nowPlayingMovies = await db.NowPlayingMovies.FindAsync(id);
            if (nowPlayingMovies == null)
            {
                return HttpNotFound();
            }
            return View(nowPlayingMovies);
        }

        // GET: NowPlayingAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NowPlayingAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NowPlayingMovies nowPlayingMovies)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/NowPlayingMovies";

                if (nowPlayingMovies.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(nowPlayingMovies.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                nowPlayingMovies.Logo = pic;
                db.NowPlayingMovies.Add(nowPlayingMovies);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return View(nowPlayingMovies);
        }

        // GET: NowPlayingAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NowPlayingMovies nowPlayingMovies = await db.NowPlayingMovies.FindAsync(id);
            if (nowPlayingMovies == null)
            {
                return HttpNotFound();
            }
            return View(nowPlayingMovies);
        }

        // POST: NowPlayingAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NowPlayingMovies nowPlayingMovies)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/NowPlayingMovies";

                if (nowPlayingMovies.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(nowPlayingMovies.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    nowPlayingMovies.Logo = pic;
                }
                db.Entry(nowPlayingMovies).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nowPlayingMovies);
        }

        // GET: NowPlayingAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NowPlayingMovies nowPlayingMovies = await db.NowPlayingMovies.FindAsync(id);
            if (nowPlayingMovies == null)
            {
                return HttpNotFound();
            }
            return View(nowPlayingMovies);
        }

        // POST: NowPlayingAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NowPlayingMovies nowPlayingMovies = await db.NowPlayingMovies.FindAsync(id);
            db.NowPlayingMovies.Remove(nowPlayingMovies);
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
