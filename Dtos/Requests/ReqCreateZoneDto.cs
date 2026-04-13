using System.ComponentModel.DataAnnotations;
using MailingApp.Utilities;

namespace MailingApp.Dtos.Requests
{
    public class ReqCreateZoneDto
    {
        [Required(ErrorMessage = Const.RESP_FAILED_MANDATORY_ZONE_NAME)]
        public string zName { get; set; }
    }
}