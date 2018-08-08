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
    public class UpComingMoviesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UpComingMovies
        public IQueryable<UpComingMovies> GetUpComingMovies()
        {
            return db.UpComingMovies;
        }

        // GET: api/UpComingMovies/5
        [ResponseType(typeof(UpComingMovies))]
        public async Task<IHttpActionResult> GetUpComingMovies(int id)
        {
            UpComingMovies upComingMovies = await db.UpComingMovies.FindAsync(id);
            if (upComingMovies == null)
            {
                return NotFound();
            }

            return Ok(upComingMovies);
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