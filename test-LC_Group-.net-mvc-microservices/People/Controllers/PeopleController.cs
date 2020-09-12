using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PeopleModule.Repositories;

namespace PeopleModule.Controllers
{    
    public class PeopleController : ApiController
    {
        private PeopleRepository peopleRepository = new PeopleRepository();

        [HttpGet]
        [Route("api/people/getInfo")]
        public IHttpActionResult GetInfo()
        {

            return Ok("123");
        }
    }
}