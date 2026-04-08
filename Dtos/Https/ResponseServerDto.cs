namespace MailingApp.Dtos.Https
{
    public record ResponseServerDto<T>(string code, string status, string category, string remark, T? data);
}