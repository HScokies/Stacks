using MongoDB.Driver;
using Stacks_rework.Data;
using Stacks_rework.Models;

namespace Stacks_rework.Services
{
    public class StacksService : AppDbContext, IStacksService
    {
        private IMongoCollection<UserStack> stacks;
        public StacksService(AppDbContext context)
        {
            stacks = context.database.GetCollection<UserStack>("decks");
        }

        public async Task CreateAsync(UserStack userStack) => await stacks.InsertOneAsync(userStack);

        public async Task<List<UserStack>> GetAsync() => await stacks.Find(_ => true).ToListAsync();
    }
}
