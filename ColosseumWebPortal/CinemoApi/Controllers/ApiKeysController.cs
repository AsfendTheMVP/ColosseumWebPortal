using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemoApi.Models;
using Microsoft.AspNet.Identity;

namespace CinemoApi.Controllers
{
    [Authorize]
    public class ApiKeysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApiKeys
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            return View(db.ApiKeys.Where(n => n.UserId == userId).ToList());
        }

        // GET: ApiKeys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiKeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KeyName,KeyString,UserId")] ApiKey apiKey)
        {
            string userId = User.Identity.GetUserId();
            apiKey.UserId = userId;

            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            apiKey.KeyString = g.ToString();

            if (ModelState.IsValid)
            {
                db.ApiKeys.Add(apiKey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apiKey);
        }

        // GET: ApiKeys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApiKey apiKey = db.ApiKeys.Find(id);
            if (apiKey == null)
            {
                return HttpNotFound();
            }
            return View(apiKey);
        }

        // POST: ApiKeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApiKey apiKey = db.ApiKeys.Find(id);
            db.ApiKeys.Remove(apiKey);
            db.SaveChanges();
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
