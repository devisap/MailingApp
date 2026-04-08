namespace MailingApp.Dtos.Https
{
    public class HttpLoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string app_access { get; set; }

        public HttpLoginDto(string userame, string password)
        {
            this.username = userame;
            this.password = password;
            this.app_access = Environment.GetEnvironmentVariable("APP_ACCESS_CODE");
        }
    }
}