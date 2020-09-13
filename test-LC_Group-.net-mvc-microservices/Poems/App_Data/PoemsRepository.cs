using System.Net.Http;

namespace PoemsModule.DataAccessLayer
{
    public class PoemsRepository
    {
        private const string URL_POEMS = "https://www.poemist.com/api/v1/randompoems/"; // поэмы

        private readonly PoemsService poemsService = new PoemsService();

        public bool SetPoem(int id)
        {
            using (var client = new HttpClient())
            {
                var result = PoemsService.ResponseResult(URL_POEMS, client);
                if (result.IsSuccessStatusCode)
                {
                    var poem = poemsService.MappingPoem(result.Content.ReadAsStringAsync().Result);
                    if (poem != null) return true;
                }
            }
            return false;
        }
    }
}