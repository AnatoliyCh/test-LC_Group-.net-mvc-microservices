using Newtonsoft.Json.Linq;
using PeopleModule.Models;
using System.Net.Http;
using System.Text.Json;

namespace PeopleModule.DataAccessLayer
{
    public class PeopleService
    {
        /// <summary> Возвращает результат запроса </summary>
        /// <param name="url">ссылка для запроса</param>
        /// <param name="client">внешний HttpClient</param>
        /// <returns>результат запроса</returns>
        public static HttpResponseMessage ResponseResult(string url, HttpClient client)
        {
            try
            {
                var response = client.GetAsync(url);
                response.Wait();
                return response.Result;
            }
            catch (System.Exception)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest };
            }            
        }

        /// <summary> Конвертирование JSON в People </summary>
        /// <param name="jsonString">JSON</param>
        /// <param name="parentNode">базовый узел JSON</param>
        /// <returns>object || null</returns>
        public People MappingPeople(string jsonString, string parentNode)
        {
            People people = null;
            var jObject = JObject.Parse(jsonString);
            people = JsonSerializer.Deserialize<People>(jObject.SelectToken(parentNode).ToString());
            people.FirstName = (string)jObject.SelectToken(parentNode + ".name.first");
            people.LastName = (string)jObject.SelectToken(parentNode + ".name.last");
            people.City = (string)jObject.SelectToken(parentNode + ".location.city");
            people.Street = (string)jObject.SelectToken(parentNode + ".location.street.name");
            people.Picture = (string)jObject.SelectToken(parentNode + ".picture.medium");
            return people;
        }
    }
}