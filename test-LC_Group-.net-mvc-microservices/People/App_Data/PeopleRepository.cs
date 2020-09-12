using Newtonsoft.Json.Linq;
using PeopleModule.Models;
using System.Net.Http;
using System.Text.Json;

namespace PeopleModule.DataAccessLayer
{
    public class PeopleRepository
    {
        private const string URL_USER = "https://randomuser.me/api/"; // пользователь
        private const string URL_QUOTE = "https://geek-jokes.sameerkumar.website/api"; // цитата
        private const string PARENT_NODE_USER = "results[0]";

        protected People MappingPeople(string jsonString)
        {
            People people = null;
            var jObject = JObject.Parse(jsonString);
            people = JsonSerializer.Deserialize<People>(jObject.SelectToken(PARENT_NODE_USER).ToString());
            people.FirstName = (string)jObject.SelectToken(PARENT_NODE_USER + ".name.first");
            people.LastName = (string)jObject.SelectToken(PARENT_NODE_USER + ".name.last");
            people.City = (string)jObject.SelectToken(PARENT_NODE_USER + ".location.city");
            people.Street = (string)jObject.SelectToken(PARENT_NODE_USER + ".location.street.name");
            people.Picture = (string)jObject.SelectToken(PARENT_NODE_USER + ".picture.medium");
            return people;
        }
        protected void SetQuote(People people, HttpClient client)
        {
            if (people == null || client == null) return;
            var response = client.GetAsync(URL_QUOTE);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode) people.Quote = result.Content.ReadAsStringAsync().Result.Replace("\"", "").Replace("\n", "");
        }

        public People GetNewPeople()
        {
            People newPeople = null;
            using (var client = new HttpClient())
            {
                // получение randomuser
                var responseUser = client.GetAsync(URL_USER);
                responseUser.Wait();

                var resultUser = responseUser.Result;
                if (resultUser.IsSuccessStatusCode)
                {
                    newPeople = MappingPeople(resultUser.Content.ReadAsStringAsync().Result); // e
                    SetQuote(newPeople, client); // получение quote
                }
            }
            return newPeople;
        }
    }
}