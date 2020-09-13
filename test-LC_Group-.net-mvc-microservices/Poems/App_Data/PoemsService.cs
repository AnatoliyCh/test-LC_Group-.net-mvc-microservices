using PoemsModule.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace PoemsModule.DataAccessLayer
{
    public class PoemsService
    {
        private readonly Random rnd = new Random();

        /// <summary> Возвращает результат запроса </summary>
        /// <param name="url">ссылка для запроса</param>
        /// <param name="client">внешний HttpClient</param>
        /// <returns>результат запроса</returns>
        public static HttpResponseMessage ResponseResult(string url, HttpClient client)
        {
            if (client == null) return null;
            var response = client.GetAsync(url);
            response.Wait();
            return response.Result;
        }

        /// <summary> Конвертирование JSON в Poem </summary>
        /// <param name="jsonString">JSON</param>
        /// <returns>object || null</returns>
        public Poem MappingPoem(string jsonString)
        {
            Poem poem = null;
            IList<Poem> poems = JsonSerializer.Deserialize<List<Poem>>(jsonString);
            // по заданию нужно вставлять одну поэму, а приходит arr[] => выбираем любую
            if (poems != null && poems.Count > 0)
            {
                poem = poems[rnd.Next(0, poems.Count - 1)];
                poem.Content = poem.Content.Replace("\n", "");
            }                
            return poem;
        }
    }
}