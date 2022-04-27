using ASP_NET_Video_Games_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Video_Games_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public IActionResult GetPublishers()
        //{
        //    var videoGamePublishers = _context.VideoGames.Select(vg => vg.Publisher).Distinct();
        //    return Ok(videoGamePublishers);
        //}

        //[HttpGet("{pubName}")]
        //public IActionResult GetGamesByPublisher(string pubName)
        //{
        //    //int? maxYear = _context.VideoGames.Select(vg => vg.Year).Max();
        //    //int? minYear = _context.VideoGames.Select(vg => vg.Year).Min();
        //    var videoGames = _context.VideoGames.Where(vg => vg.Publisher == pubName);
        //    return Ok(videoGames);
        //}

        [HttpGet("{id}")]

        public IActionResult GetGamesById(int id)
        {

            var videoGames = _context.VideoGames.Where(vg => vg.Id == id);
            return Ok(videoGames);

        }

        [HttpGet]

        public IActionResult GetGames()
        {
            var videoGames = _context.VideoGames;
            return Ok(videoGames);
        }


        [HttpGet("gamesByConsole")]

        public IActionResult GetSalesByConsole()
        {

            var consoles = _context.VideoGames.Select(c => c.Platform).Distinct();

            Dictionary<string, double> returnValue = new Dictionary<string, double>();
            foreach (string Platform in consoles.ToList())
            {
                var salesPerConsole = _context.VideoGames.Where(i => i.Platform == Platform).Where(vg => vg.Year > 2013).Select(i => i.GlobalSales).Sum();
                returnValue.Add(Platform, salesPerConsole);
            }
            return Ok(returnValue);
        }
    }
}
