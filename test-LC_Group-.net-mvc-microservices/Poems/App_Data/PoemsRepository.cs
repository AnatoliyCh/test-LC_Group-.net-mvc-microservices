using System.Net.Http;
using PeopleModule.DataAccessLayer;

namespace PoemsModule.DataAccessLayer
{
    public class PoemsRepository
    {
        private const string URL_POEMS = "https://www.poemist.com/api/v1/randompoems/"; // поэмы

        private readonly PeopleContext peopleDB = new PeopleContext();
        private readonly PoemsService poemsService = new PoemsService();

        public bool SetPoem(int id)
        {
            using (var client = new HttpClient())
            {
                var result = PoemsService.ResponseResult(URL_POEMS, client);
                if (result.IsSuccessStatusCode)
                {
                    var poem = poemsService.MappingPoem(result.Content.ReadAsStringAsync().Result);
                    if (poem != null)
                    {
                        var people = peopleDB.Peoples.Find(id);
                        if (people != null)
                        {
                            people.Poem = poem.Content;
                            people.Distance = poemsService.GetDistance(poem.Content);
                            peopleDB.Entry(people).State = System.Data.Entity.EntityState.Modified; // объект есть в бд, надо его модифицировать
                            peopleDB.SaveChanges();
                            return true;
                        }                        
                    }
                }
            }
            return false;
        }
    }
}