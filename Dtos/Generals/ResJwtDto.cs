using MailingApp.Services;

namespace MailingApp.Dtos.Generals
{
    public class ResJWTDto
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

        public ResJWTDto(TokenObject tObject)
        {
            u_id = tObject.u_id;
            u_username = tObject.u_username;
            u_fullname = tObject.u_fullname;
            u_email = tObject.u_email;
            u_img = tObject.u_img;
            u_telp = tObject.u_telp;
            u_address = tObject.u_address;
            dep_id = tObject.dep_id;
            dep_name = tObject.dep_name;
            div_id = tObject.div_id;
            div_name = tObject.div_name;
            roles = tObject.roles;
            u_is_internal = tObject.u_is_internal;
            x_key = tObject.x_key;
        }
    }

    public class RoleDto
    {
        public string r_id { get; set; }
        public string r_category { get; set; }

        public RoleDto(string rId, string rCategory)
        {
            r_id = rId;
            r_category = rCategory;
        }
    }
}