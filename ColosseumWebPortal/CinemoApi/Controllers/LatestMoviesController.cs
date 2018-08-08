using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinemoApi.Models;

namespace CinemoApi.Controllers
{
    public class LatestMoviesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LatestMovies
        public IQueryable<Latest> GetLatests()
        {
            return db.Latests;
        }

        // GET: api/LatestMovies/5
        [ResponseType(typeof(Latest))]
        public async Task<IHttpActionResult> GetLatest(int id)
        {
            Latest latest = await db.Latests.FindAsync(id);
            if (latest == null)
            {
                return NotFound();
            }

            return Ok(latest);
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