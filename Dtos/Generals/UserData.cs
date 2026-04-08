using MailingApp.Dtos.Generals;

namespace MailingApp.Dtos.Generals
{
    public record UserData(string? uId, string? uUsername, string? uEmail, string? uFullname, string? uAddress, string? uTelp, string? depName, RoleDto? roleDto, byte? uIsInternal, string? xKey);
}