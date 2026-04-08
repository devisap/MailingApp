using System.ComponentModel.DataAnnotations;

namespace MailingApp.Dtos.Requests
{
    public class ReqLoginDto 
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}