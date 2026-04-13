using System.Text.Json.Serialization;

namespace MailingApp.Dtos.Responses
{
    public class ResAreaDto
    {
        public int areId { get; set; }
        public int zId { get; set; }
        public string zName { get; set; }
        public string areName { get; set; }

        [JsonIgnore]
        public byte areIsActive { get; set; }
        [JsonIgnore]
        public DateTime? createdAt { get; set; }
    }
}