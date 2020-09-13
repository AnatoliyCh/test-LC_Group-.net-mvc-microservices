using System.ComponentModel.DataAnnotations.Schema;
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
        public double Distance { get; set; }

        [NotMapped]
        public bool Empty
        {
            get
            {
                return (Id == 0 &&
                        string.IsNullOrWhiteSpace(Gender) &&
                        string.IsNullOrWhiteSpace(FirstName) &&
                        string.IsNullOrWhiteSpace(LastName) &&
                        string.IsNullOrWhiteSpace(City) &&
                        string.IsNullOrWhiteSpace(Street) &&
                        string.IsNullOrWhiteSpace(Email) &&
                        string.IsNullOrWhiteSpace(Picture) &&
                        string.IsNullOrWhiteSpace(Poem) &&
                        string.IsNullOrWhiteSpace(Quote));
            }
        }
    }
}