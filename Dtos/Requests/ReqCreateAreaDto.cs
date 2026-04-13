using System.ComponentModel.DataAnnotations;
using MailingApp.Utilities;

namespace MailingApp.Dtos.Requests
{
    public class ReqCreateAreaDto
    {
        [Required(ErrorMessage = Const.RESP_FAILED_MANDATORY_ZONE_ID)]
        public int zId { get; set; }
        [Required(ErrorMessage = Const.RESP_FAILED_MANDATORY_AREA_NAME)]
        public string areName { get; set; }
    }
}