using MailingApp.Utilities;

namespace MailingApp.Dtos.Generals
{
    public class ResSuccessDto
    {
        public ushort httpCode { get; set; } = Const.HTTP_CODE_SUCCESS;
        public string status { get; set; } = Const.RES_SUCCESS;
        public string category { get; set; }
        public string remark { get; set; }
        public dynamic data { get; set; }

        public ResSuccessDto(ResStatusDto resStatus, dynamic data)
        {
            this.category = resStatus.category;
            this.remark = resStatus.remark;
            this.data = data;
        }
    }
}