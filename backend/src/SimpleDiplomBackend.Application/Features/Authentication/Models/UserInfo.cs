namespace SimpleDiplomBackend.Application.Features.Authentication.Models
{
    public class UserInfo: AppUser
    {
        public string Id { get; set; } = string.Empty;
        public string[] Roles { get; set; } = { };
        public bool Locked { get; set; }
    }
}