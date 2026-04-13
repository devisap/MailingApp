using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailingApp.Models.Entites
{
    [Table("zone", Schema = "master")]
    public class Zone
    {
        [Key]
        public int z_id {get;set;}
        public string z_name {get;set;}
        public byte z_is_active {get;set;}
        public DateTime? created_at { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string? updated_by { get; set; }
        public DateTime? deleted_at { get; set; }
        public string? deleted_by { get; set; }
    }
}