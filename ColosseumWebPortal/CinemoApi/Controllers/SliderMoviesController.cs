using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinemoApi.Models;

namespace CinemoApi.Controllers
{
    public class SliderMoviesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SliderMovies
        public IQueryable<SliderMovie> GetSliderMovies()
        {
            return db.SliderMovies;
        }

        // GET: api/SliderMovies/5
        [ResponseType(typeof(SliderMovie))]
        public async Task<IHttpActionResult> GetSliderMovie(int id)
        {
            SliderMovie sliderMovie = await db.SliderMovies.FindAsync(id);
            if (sliderMovie == null)
            {
                return NotFound();
            }

            return Ok(sliderMovie);
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