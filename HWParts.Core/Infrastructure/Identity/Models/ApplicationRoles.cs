namespace HWParts.Core.Infrastructure.Identity.Models
{
    public static class ApplicationRoles
    {
        public const string Super = "Super";
        public const string Admin = "Admin";
        public const string Moderator = "Moderator";
        public const string Common = "Common";

        public const string AllRoles = Super + "," + Admin + "," + Moderator + "," + Common;
        public const string StaffRoles = Super + "," + Admin + "," + Moderator;
    }
}
