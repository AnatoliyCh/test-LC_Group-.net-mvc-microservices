using PeopleModule.DataAccessLayer;
using System.Web.Http;

namespace PeopleModule.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly PeopleRepository peopleRepository = new PeopleRepository();

        [HttpGet, Route("api/people/getInfo")]
        public IHttpActionResult GetInfo()
        {
            var result = peopleRepository.GetNewPeople();
            if (result != null) return Ok(result.Id);
            return BadRequest();
        }
    }
}