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
            if (res!=null && res.token != userStack.token) throw new DatabaseException(StatusCodes.Status409Conflict);
            await userStacks.InsertOneAsync(userStack);
            return userStack;
        }

        public async Task<AdvertisementStack> CreateAdvertisementStack(AdvertisementStack adStack, string companyToken)
        {
            if (companyToken != adStack.token) throw new DatabaseException(StatusCodes.Status400BadRequest);
            var res = await organisations.Find(o => o.token == companyToken).FirstOrDefaultAsync();
            if (res is null) throw new DatabaseException(StatusCodes.Status401Unauthorized);
            await advertisementStacks.InsertOneAsync(adStack);
            return adStack;
        }

        public async Task DropUserStack(string id, string ownerid, string token)
        {
            var res = await userStacks.Find(s => s.id == id && s.uid == ownerid).FirstOrDefaultAsync();
            if (res is null) throw new DatabaseException(StatusCodes.Status404NotFound);
            if (res.token != token) throw new DatabaseException(StatusCodes.Status401Unauthorized);
            await userStacks.DeleteOneAsync(s => s == res);
        }

        public async Task<List<StackPreview>> GetAdsPreview()
        {
            var orgs = await organisations.Find(_ => true).ToListAsync();

            List<StackPreview> adPreview = new();
            foreach (var org in orgs)
            {
                var count = await advertisementStacks.Find(s => s.organisation == org && s.isActive == true).CountDocumentsAsync();
                if (count == 0) continue;
                StackPreview stack = new(
                    name:   org.name, 
                    thumb:  org.logo,
                    amount: count);

                adPreview.Add(stack);
            }
            return adPreview;
        }

        public async Task<AdvertisementStack> GetAdStack(string id)
        {
            var res = await advertisementStacks.Find(s => s.id == id).FirstOrDefaultAsync();
            if (res is null) throw new DatabaseException(StatusCodes.Status404NotFound);
            if (res.isActive == false) throw new DatabaseException(StatusCodes.Status423Locked);
            return res;
        }

        public async Task<List<FriendsPreview>> GetFriendsPreview(IEnumerable<string> friendIds)
        {
            List<FriendsPreview> friendsPreview = new();
            foreach(var friendId in friendIds)
            {
                var count = await userStacks.Find(s => s.uid == friendId && s.isPrivate == false).CountDocumentsAsync();
                if (count == 0) continue;
                FriendsPreview friendPreview = new(
                    uid: friendId,
                    amount: count
                    );

                friendsPreview.Add(friendPreview);
            }
            return friendsPreview;
        }

        public async Task<UserStack> GetUserStack(string id, string uid, string token)
        {
            var res = await userStacks.Find(s => s.id == id).FirstOrDefaultAsync();
            if (res is null) throw new DatabaseException(StatusCodes.Status404NotFound);
            if (res.isPrivate == false) return res;
            if (res.uid != uid || res.token != token) throw new DatabaseException(StatusCodes.Status403Forbidden);
            return res;
        }

        public async Task<List<StackPreview>> ListFriendStacks(string uid)
        {
            var stacks = await userStacks.Find(s => s.uid == uid && s.isPrivate == false).ToListAsync();
            List<StackPreview> stacksPreview = new();
            foreach (var stack in stacks)
            {
                StackPreview stackPreview = new(
                    name: stack.name,
                    thumb: stack.thumbnail,
                    amount: stack.cards.Count()
                );
                stacksPreview.Add(stackPreview);
            }
            return stacksPreview;
        }

        public async Task<List<StackPreview>> ListOrgStacks(string orgId)
        {
            var stacks = await advertisementStacks.Find(s => s.organisation.id == orgId && s.isActive == true).ToListAsync();

            List<StackPreview> stacksPreview = new();
            foreach (var stack in stacks)
            {
                StackPreview stackPreview = new(
                    name: stack.name,
                    thumb: stack.thumbnail,
                    amount: stack.cards.Count()
                );
                stacksPreview.Add(stackPreview);
            }
            return stacksPreview;
        }

        public async Task<List<StackPreview>> ListPersonalStacks(string token, string uid)
        {
            var stacks = await userStacks.Find(s => s.uid == uid && s.token == token).ToListAsync();
            List<StackPreview> stacksPreview = new();
            foreach(var stack in stacks)
            {
                StackPreview stackPreview = new(
                    name: stack.name,
                    thumb: stack.thumbnail,
                    amount: stack.cards.Count()
                );
                stacksPreview.Add(stackPreview);
            }
            return stacksPreview;
        }

        public async Task<AdvertisementStack> UpdateOrgStack(string id, string ownerid, string token, UpdateStack newStack)
        {
            var res = await advertisementStacks.Find(s => s.id == id).FirstOrDefaultAsync();
            if (res is null) throw new DatabaseException(StatusCodes.Status404NotFound);
            if (res.token != token || res.organisation.id != ownerid) throw new DatabaseException(StatusCodes.Status403Forbidden);
            res = newStack.AutoMap<UpdateStack, AdvertisementStack>();
            await advertisementStacks.ReplaceOneAsync(s => s.id == id, res);
            return res;
        }

        public async Task<UserStack> UpdateUserStack(string id, string ownerid, string token, UpdateStack newStack)
        {
            var res = await userStacks.Find(s => s.id == id).FirstOrDefaultAsync();
            if (res is null) throw new DatabaseException(StatusCodes.Status404NotFound);
            if (res.token != token || res.uid != ownerid) throw new DatabaseException(StatusCodes.Status403Forbidden);
            res = newStack.AutoMap<UpdateStack, UserStack>();
            await userStacks.ReplaceOneAsync(s => s.id == id, res);
            return res;
        }
    }
}
