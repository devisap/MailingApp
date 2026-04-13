using System.ComponentModel.DataAnnotations;
using MailingApp.Utilities;

namespace MailingApp.Dtos.Requests
{
    public class ReqUpdateZoneDto
    {
        [Required(ErrorMessage = Const.RESP_FAILED_MANDATORY_ID_DATA)]
        public int id { get; set; }
        [Required(ErrorMessage = Const.RESP_FAILED_MANDATORY_ZONE_NAME)]
        public string zName { get; set; }
    }
}