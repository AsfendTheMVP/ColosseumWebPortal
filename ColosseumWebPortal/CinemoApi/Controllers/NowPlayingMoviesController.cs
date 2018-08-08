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
    public class NowPlayingMoviesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NowPlayingMovies
        public IQueryable<NowPlayingMovies> GetNowPlayingMovies()
        {
            return db.NowPlayingMovies;
        }

        // GET: api/NowPlayingMovies/5
        [ResponseType(typeof(NowPlayingMovies))]
        public async Task<IHttpActionResult> GetNowPlayingMovies(int id)
        {
            NowPlayingMovies nowPlayingMovies = await db.NowPlayingMovies.FindAsync(id);
            if (nowPlayingMovies == null)
            {
                return NotFound();
            }

            return Ok(nowPlayingMovies);
        }

        // PUT: api/NowPlayingMovies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNowPlayingMovies(int id, NowPlayingMovies nowPlayingMovies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nowPlayingMovies.MovieId)
            {
                return BadRequest();
            }

            db.Entry(nowPlayingMovies).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NowPlayingMoviesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/NowPlayingMovies
        [ResponseType(typeof(NowPlayingMovies))]
        public async Task<IHttpActionResult> PostNowPlayingMovies(NowPlayingMovies nowPlayingMovies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NowPlayingMovies.Add(nowPlayingMovies);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nowPlayingMovies.MovieId }, nowPlayingMovies);
        }

        // DELETE: api/NowPlayingMovies/5
        [ResponseType(typeof(NowPlayingMovies))]
        public async Task<IHttpActionResult> DeleteNowPlayingMovies(int id)
        {
            NowPlayingMovies nowPlayingMovies = await db.NowPlayingMovies.FindAsync(id);
            if (nowPlayingMovies == null)
            {
                return NotFound();
            }

            db.NowPlayingMovies.Remove(nowPlayingMovies);
            await db.SaveChangesAsync();

            return Ok(nowPlayingMovies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NowPlayingMoviesExists(int id)
        {
            return db.NowPlayingMovies.Count(e => e.MovieId == id) > 0;
        }
    }
}