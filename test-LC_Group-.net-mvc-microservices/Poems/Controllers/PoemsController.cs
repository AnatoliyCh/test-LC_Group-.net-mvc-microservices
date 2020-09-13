using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PoemsModule.Controllers
{
    public class PoemsController : ApiController
    {
        [HttpGet]
        [Route("api/poems/getPoem/{id}")]
        public IHttpActionResult GetPoem(int id)
        {            
            return Ok();
        }
    }
}