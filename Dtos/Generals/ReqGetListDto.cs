using System.ComponentModel.DataAnnotations;
using MailingApp.Utilities;

namespace MailingApp.Dtos.Generals
{
    public class ReqGetListDto
    {
        [Required(ErrorMessage = Const.RESP_FAILED_MANDATORY_PAGE_LIMIT)]
        public int pageLimit { get; set; }
        [Required(ErrorMessage = Const.RESP_FAILED_MANDATORY_PAGE_NUMBER)]
        public int pageNumber { get; set; }
        public string? search { get; set; }
        public string? orderBy { get; set; }
        public string? ordering { get; set; }
    }
}