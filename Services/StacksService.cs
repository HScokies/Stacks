using MongoDB.Driver;
using Stacks_rework.Data;
using Stacks_rework.Models;
using Stacks_rework.Models.Output;

namespace Stacks_rework.Services
{
    public class StacksService : AppDbContext, IStacksService
    {
        private IMongoCollection<UserStack> userStacks;
        private IMongoCollection<AdvertisementStack> advertisementStacks;
        private IMongoCollection<Organisation> organisations;
        public StacksService(AppDbContext context)
        {
            userStacks = context.database.GetCollection<UserStack>("userStacks");
            advertisementStacks = context.database.GetCollection<AdvertisementStack>("adStacks");
            organisations = context.database.GetCollection<Organisation>("organisations");
        }

        public async Task<UserStack> Create(UserStack userStack)
        {
            var res = await userStacks.Find(u => u.uid == userStack.uid).FirstOrDefaultAsync();
            if (res.token != userStack.token) throw new DatabaseException(StatusCodes.Status409Conflict);

            await userStacks.InsertOneAsync(userStack);

            return userStack;
        }

        public async Task<AdvertisementStack> CreateAdvertisementStack(AdvertisementStack adStack, string companyToken)
        {
            if (companyToken != adStack.token) throw new DatabaseException(StatusCodes.Status400BadRequest);
            var res = await organisations.Find(o => o.token == companyToken).FirstOrDefaultAsync();
            if (res is null) throw new DatabaseException(StatusCodes.Status404NotFound);

            await advertisementStacks.InsertOneAsync(adStack);

            return adStack;
        }

        public async Task<List<FriendsPreview>> GetFriendsPreview(IEnumerable<string> friendIDs)
        {
            List<FriendsPreview> friends = new();
            foreach (var uid in friendIDs)
            {
                var numberOfStacks = await userStacks.Find(u => u.uid == uid && u.isPrivate == false).CountDocumentsAsync();
                if (numberOfStacks == 0) continue;
                friends.Add(new FriendsPreview(
                    uid: uid,
                    amount: numberOfStacks
                ));
            }
            return friends;
        }

        public async Task<List<AdvertisementStack>> ListAdvertisementStacks(string orgId) => await advertisementStacks.Find(s => s.organisation.id == orgId).ToListAsync();
        public async Task<List<UserStack>> ListPersonalStacks(string token, string uid) => await userStacks.Find(S => S.token == token && S.uid == uid).ToListAsync();

        public async Task<List<UserStack>> ListPublicStacks(string uid) => await userStacks.Find(s => s.uid == uid && s.isPrivate == false).ToListAsync();

        public async Task<List<StackPreview>> PreviewFriendStack(string uid)
        {
            var stacks = await userStacks.Find(s => s.uid == uid && s.isPrivate == false).ToListAsync();
            List<StackPreview> preview = new();
            foreach(var stack in stacks)
            {
                preview.Add(new StackPreview(
                    name: stack.name,
                    thumb: stack.thumbnail,
                    amount: stack.cards.Count()
                ));
            }
            return preview;
        }

        public Task<List<StackPreview>> PreviewPersonalStack(string uid, string token)
        {
            throw new NotImplementedException();
        }
    }
}
