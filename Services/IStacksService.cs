using Stacks_rework.Models;
using Stacks_rework.Models.Output;

namespace Stacks_rework.Services
{
    public interface IStacksService
    {
        public Task<UserStack> Create(UserStack userStack);
        public Task<AdvertisementStack> CreateAdvertisementStack(AdvertisementStack adStack, string companyToken);
        public Task<List<UserStack>> ListPersonalStacks(string token, string uid);
        public Task<List<UserStack>> ListPublicStacks(string uid);
        public Task<List<AdvertisementStack>> ListAdvertisementStacks(string orgId);
        public Task<List<FriendsPreview>> GetFriendsPreview(IEnumerable<string> friends);
        public Task<List<StackPreview>> PreviewFriendStack(string uid);
        public Task<List<StackPreview>> PreviewPersonalStack(string uid, string token);
    }
}
