using MailingApp.Utilities;

namespace MailingApp.Dtos.Generals
{
    public class ResErrorDto
    {
        public ushort httpCode { get; set; } = Const.HTTP_CODE_INTERNAL_SERVER_ERROR;
        public string status { get; set; } = Const.RES_ERROR;
        public string category { get; set; }
        public string remark { get; set; }
        public string? data { get; set; }

        public ResErrorDto(ResStatusDto resStatus, string? data)
        {
            this.category = resStatus.category;
            this.remark = resStatus.remark;
        }
    }
}