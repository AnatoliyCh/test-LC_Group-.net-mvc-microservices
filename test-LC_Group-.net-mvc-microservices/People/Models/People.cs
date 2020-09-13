using System.Text.Json.Serialization;

namespace PeopleModule.Models
{
    public class People
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        public string Picture { get; set; }
        public string Poem { get; set; }
        public string Quote { get; set; }
        public string Distance { get; set; }
    }
}