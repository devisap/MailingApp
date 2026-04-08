using Newtonsoft.Json;
using MailingApp.Dtos.Https;
using MailingApp.Dtos.Requests;
using MailingApp.Dtos.Generals;
using MailingApp.Utilities;

namespace MailingApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly PermissionUtil _permissionUtil;

        public AuthService(PermissionUtil permissionUtil)
        {
            _permissionUtil = permissionUtil;
        }

        public ResponseServerDto<DataToken> CheckAuthentication(ReqLoginDto reqDto)
        {
            HttpResponseMessage response = _httpClient.PostAsJsonAsync(Environment.GetEnvironmentVariable("URL_AUTHENTICATION_LOGIN"), new HttpLoginDto(reqDto.username, reqDto.password)).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            var resp = JsonConvert.DeserializeObject<ResponseServerDto<DataToken>>(responseBody);
            return resp;
        }

        public string GenerateToken(string token)
        {
            Console.WriteLine(token);
            var tokenDecoded = _permissionUtil.DecodeAppAccessToken(token);
            var tObject = JsonConvert.DeserializeObject<TokenObject>(tokenDecoded);

            ResJWTDto resJwt = new ResJWTDto(tObject);

            return _permissionUtil.GenerateJWTToken(resJwt);
        }

    }

    public class DataToken
    {
        public string token { get; set; }
    }
    
    public class TokenObject 
    {
        public string u_id { get; set; }
        public string u_username { get; set; }
        public string u_fullname { get; set; }
        public string u_email { get; set; }
        public string? u_img { get; set; }
        public string? u_telp { get; set; }
        public string? u_address { get; set; }
        public string? dep_id { get; set; }
        public string? dep_name { get; set; }
        public string? div_id { get; set; }
        public string? div_name { get; set; }
        public List<RoleDto> roles { get; set; }
        public ushort u_is_internal { get; set; }
        public string x_key { get; set; }

    }
}
