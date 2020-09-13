using PeopleModule.Models;
using System.Net.Http;

namespace PeopleModule.DataAccessLayer
{
    public class PeopleRepository
    {
        private const string URL_USER = "https://randomuser.me/api/"; // пользователь
        private const string URL_QUOTE = "https://geek-jokes.sameerkumar.website/api"; // цитата
        private const string PARENT_NODE_USER = "results[0]";

        private readonly PeopleContext peopleDB = new PeopleContext();
        private readonly PeopleService peopleService = new PeopleService();

        protected void SetQuote(People people, HttpClient client)
        {
            if (people == null || client == null) return;
            var result = PeopleService.ResponseResult(URL_QUOTE, client); // получение quote
            if (result.IsSuccessStatusCode) people.Quote = result.Content.ReadAsStringAsync().Result.Replace("\"", "").Replace("\n", "");
        }

        public People GetNewPeople()
        {
            People newPeople = null;
            using (var client = new HttpClient())
            {
                var resultUser = PeopleService.ResponseResult(URL_USER, client); // получение randomuser
                if (resultUser.IsSuccessStatusCode)
                {
                    newPeople = peopleService.MappingPeople(resultUser.Content.ReadAsStringAsync().Result, PARENT_NODE_USER);
                    SetQuote(newPeople, client);
                    if (newPeople != null)
                    {
                        newPeople.Id = peopleDB.Peoples.Add(newPeople).Id;
                        peopleDB.SaveChanges();
                    }
                }
            }
            return newPeople;
        }
    }
}