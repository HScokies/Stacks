using Stacks_rework.Models;

namespace Stacks_rework.Services
{
    public static class Helpers
    {
        public static UserStack hideToken(UserStack userStack)
        {
            userStack.token = null!;
            return userStack;
        }
        public static AdvertisementStack hideToken(AdvertisementStack adStack)
        {
            adStack.organization = null!; adStack.token = null!; return adStack;
        }
        public static bool ValidateAuth(string uid, string guid)
        {
            if (uid.Length != 9) return false;
            if (!Guid.TryParse(guid, out _)) return false;
            if (!int.TryParse(uid, out _)) return false;
            return true;
        }
        public static bool ValidateAuth(string guid)
        {
            if (!Guid.TryParse(guid, out _)) return false;
            return true;
        }
    }
}
