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
    public class SliderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Slider
        public async Task<ActionResult> Index()
        {
            return View(await db.SliderMovies.ToListAsync());
        }

        // GET: Slider/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderMovie sliderMovie = await db.SliderMovies.FindAsync(id);
            if (sliderMovie == null)
            {
                return HttpNotFound();
            }
            return View(sliderMovie);
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SliderMovie sliderMovie)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/SliderMovies";

                if (sliderMovie.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(sliderMovie.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                sliderMovie.Logo = pic;
                db.SliderMovies.Add(sliderMovie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sliderMovie);
        }

        // GET: Slider/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderMovie sliderMovie = await db.SliderMovies.FindAsync(id);
            if (sliderMovie == null)
            {
                return HttpNotFound();
            }
            return View(sliderMovie);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SliderMovie sliderMovie)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/SliderMovies";
                if (sliderMovie.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(sliderMovie.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    sliderMovie.Logo = pic;
                }
                db.Entry(sliderMovie).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sliderMovie);
        }

        // GET: Slider/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderMovie sliderMovie = await db.SliderMovies.FindAsync(id);
            if (sliderMovie == null)
            {
                return HttpNotFound();
            }
            return View(sliderMovie);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SliderMovie sliderMovie = await db.SliderMovies.FindAsync(id);
            db.SliderMovies.Remove(sliderMovie);
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
