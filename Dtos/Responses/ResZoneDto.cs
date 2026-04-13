using System.Text.Json.Serialization;

namespace MailingApp.Dtos.Responses
{
    public class ResZoneDto
    {
        public int zId { get; set; }
        public string zName { get; set; }

        [JsonIgnore]
        public byte zIsActive { get; set; }
        [JsonIgnore]
        public DateTime? createdAt { get; set; }
    }
}