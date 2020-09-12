using PeopleModule.DataAccessLayer;
using System.Web.Http;

namespace PeopleModule.Controllers
{
    public class PeopleController : ApiController
    {
        private PeopleRepository peopleRepository = new PeopleRepository();

        [HttpGet]
        [Route("api/people/getInfo")]
        public IHttpActionResult GetInfo()
        {
            var a = peopleRepository.GetNewPeople();
            return Ok(a);
        }
    }
}