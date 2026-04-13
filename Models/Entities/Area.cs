using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailingApp.Models.Entites
{
    [Table("area", Schema = "master")]
    public class Area
    {
        [Key]
        public int are_id { get; set; }
        public int z_id { get; set; }
        public string are_name { get; set; }
        public byte are_is_active { get; set; }
        public DateTime? created_at { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public string? deleted_by { get; set; }
    }
}