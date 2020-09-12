using PeopleModule.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace PeopleModule.Repositories
{
    public class PeopleRepository
    {
        public async Task<People> GetNewPeople()
        {
            People tmpNewPeople = null;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode) tmpNewPeople = await response.Content.ReadAsAsync<People>();
            }

            return tmpNewPeople;
        }
    }
}