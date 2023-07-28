using Stacks_rework.Models;
using Stacks_rework.Models.Input;

namespace Stacks_rework.Services
{
    public interface IStacksService
    {
        public Task CreateAsync(UserStack userStack);
        public Task<List<UserStack>> GetAsync();
    }
}
