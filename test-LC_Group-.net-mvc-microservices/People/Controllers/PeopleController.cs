using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace People.Controllers
{
    public class PeopleController : ApiController
    {
        [HttpGet]
        [Route("api/people/getInfo")]
        public IHttpActionResult GetInfo()
        {
            return Ok("123");
        }
    }
}