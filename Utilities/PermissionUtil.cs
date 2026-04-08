using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Newtonsoft.Json;
using MailingApp.Dtos.Generals;

namespace MailingApp.Utilities
{
    public class PermissionUtil
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionUtil(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateJWTToken(ResJWTDto resDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var privateKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var keyBytes = Encoding.UTF8.GetBytes(privateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(Const.JWT_TOKEN_EXPIRE),
                Subject = GenerateClaims(resDto)
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private ClaimsIdentity GenerateClaims(ResJWTDto resDto)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim("u_id", resDto.u_id ?? ""));
            ci.AddClaim(new Claim("u_username", resDto.u_username ?? ""));
            ci.AddClaim(new Claim("u_fullname", resDto.u_fullname ?? ""));
            ci.AddClaim(new Claim("u_email", resDto.u_email ?? ""));
            ci.AddClaim(new Claim("u_img", resDto.u_img ?? ""));
            ci.AddClaim(new Claim("u_telp", resDto.u_telp ?? ""));
            ci.AddClaim(new Claim("u_address", resDto.u_address ?? ""));
            ci.AddClaim(new Claim("dep_id", resDto.dep_id ?? ""));
            ci.AddClaim(new Claim("dep_name", resDto.dep_name ?? ""));
            ci.AddClaim(new Claim("div_id", resDto.div_id ?? ""));
            ci.AddClaim(new Claim("div_name", resDto.div_name ?? ""));

            if (resDto.roles == null)
            {
                ci.AddClaim(new Claim("role_id", ""));
                ci.AddClaim(new Claim("role_category", ""));
            }
            else
            {
                ci.AddClaim(new Claim("role_id", resDto.roles[0].r_id));
                ci.AddClaim(new Claim("role_category", resDto.roles[0].r_category));
            }

            ci.AddClaim(new Claim("u_is_internal", resDto.u_is_internal.ToString()));
            ci.AddClaim(new Claim("x_key", resDto.x_key ?? ""));

            return ci;
        }
        public string DecodeAppAccessToken(string accessToken)
        {
            try
            {
                string key = Environment.GetEnvironmentVariable("APP_ACCESS_KEY");
                string cipher = Environment.GetEnvironmentVariable("CRYPT_CHIPER");
                string algorithm = Environment.GetEnvironmentVariable("CRYPT_ALGORITHM");

                // Konversi base64 input ke byte array
                byte[] c = Convert.FromBase64String(accessToken);

                // Panjang IV berdasarkan cipher yang digunakan
                int ivLen = 16; // 16 byte untuk AES-128-CBC
                byte[] iv = new byte[ivLen];
                Array.Copy(c, 0, iv, 0, ivLen);

                // Panjang HMAC (SHA-256 menghasilkan 32 byte hash)
                int hmacLen = 32;
                byte[] hmac = new byte[hmacLen];
                Array.Copy(c, ivLen, hmac, 0, hmacLen);

                // Ambil ciphertext raw
                byte[] ciphertextRaw = new byte[c.Length - ivLen - hmacLen];
                Array.Copy(c, ivLen + hmacLen, ciphertextRaw, 0, ciphertextRaw.Length);

                // Dekripsi ciphertext raw menggunakan AES
                string originalPlainText;
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertextRaw, 0, ciphertextRaw.Length);
                        originalPlainText = Encoding.UTF8.GetString(decryptedBytes);
                    }
                }

                // Validasi HMAC
                using (var hmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
                {
                    byte[] calcMac = hmacSha256.ComputeHash(ciphertextRaw);
                    if (!CompareMac(hmac, calcMac))
                    {
                        throw new Exception("MAC validation failed.");
                    }
                }

                return originalPlainText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public UserData RetrieveUser()
        {
            var uId = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "u_id")?.Value;
            var uUsername = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "u_username")?.Value;
            var uEmail = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "u_email")?.Value;
            var uFullname = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "u_fullname")?.Value;
            var uAddress = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "u_address")?.Value;
            var uTelp = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "u_telp")?.Value;
            var depName = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "dep_name")?.Value;
            var roleId = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "role_id")?.Value;
            var roleCategory = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "role_category")?.Value;
            var xKey = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "x_key")?.Value;

            var uIsInternalClaim = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "u_is_internal")?.Value;
            byte? uIsInternal = null;

            if (byte.TryParse(uIsInternalClaim, out var parsedIsInternal))
            {
                uIsInternal = parsedIsInternal;
            }
            
            RoleDto role = new RoleDto(roleId, roleCategory);

            return new UserData(uId, uUsername, uEmail, uFullname, uAddress, uTelp, depName, role, uIsInternal, xKey);
        }

        private bool CompareMac(byte[] mac1, byte[] mac2)
        {
            if (mac1.Length != mac2.Length) return false;

            int result = 0;
            for (int i = 0; i < mac1.Length; i++)
            {
                result |= mac1[i] ^ mac2[i];
            }
            return result == 0;
        }
    }
}