using MailingApp.Utilities;

namespace MailingApp.Dtos.Generals
{
    public class ResFailedDto
    {
        public ushort httpCode { get; set; }
        public string status { get; set; } = Const.RES_FAILED;
        public string category { get; set; }
        public string remark { get; set; } 
        public dynamic data { get; set; }

        public ResFailedDto(ResStatusDto resStatus, ushort httpCode, dynamic data)
        {
            this.category = resStatus.category;
            this.remark = resStatus.remark;
            this.httpCode = httpCode;
            this.data = data;
        }
    }
}
