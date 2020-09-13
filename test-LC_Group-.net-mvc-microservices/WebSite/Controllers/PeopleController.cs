using PeopleModule.DataAccessLayer;
using PeopleModule.Models;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class PeopleController : Controller
    {
        private PeopleContext db = new PeopleContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReloadPage(People people)
        {
            return View("Index", people.Empty ? null : people);
        }

        public ActionResult GetInfo()
        {
            People people = null;
            using (var client = new HttpClient())
            {
                var result = PeopleService.ResponseResult("http://localhost:5081/api/people/getInfo", client);
                if (result.IsSuccessStatusCode)
                {
                    string strId = result.Content.ReadAsStringAsync().Result;
                    
                    int id = -1;
                    if (int.TryParse(strId, out id))
                    {
                        people = db.Peoples.Find(id);
                    }
                }
            }
            
            return View("Index", people);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
