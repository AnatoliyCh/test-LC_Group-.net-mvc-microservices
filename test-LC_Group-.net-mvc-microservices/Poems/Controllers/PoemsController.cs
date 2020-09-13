using PoemsModule.DataAccessLayer;
using System.Web.Http;

namespace PoemsModule.Controllers
{
    public class PoemsController : ApiController
    {
        private readonly PoemsRepository poemsRepository = new PoemsRepository();

        [HttpGet, Route("api/poems/getPoem/{id}")]
        public IHttpActionResult GetPoem(int id)
        {
            var result = poemsRepository.SetPoem(id);
            if (result) return Ok(result);
            return BadRequest();
        }
    }
}