using Stacks_rework.Models;
using Stacks_rework.Models.Output;

namespace Stacks_rework.Services
{
    public interface IStacksService
    {
        #region Основной функционал
        public Task<UserStack> Create(UserStack userStack);
        public Task<AdvertisementStack> CreateAdvertisementStack(AdvertisementStack adStack);

        public Task<UserStack> GetUserStack(string id, string uid, string token);
        public Task<AdvertisementStack> GetAdStack(string id, string token = null);

        public Task<UserStack> UpdateUserStack(string id, string ownerid, string token, UpdateStack newStack);
        public Task<AdvertisementStack> UpdateOrgStack(string id, string token, UpdateStack newStack);

        public Task DropUserStack(string id, string ownerid, string token);
        public Task DropOrgStack(string id, string token);
        #endregion

        #region Главная Страница
        public Task<List<StackPreview>> GetAdsPreview(); // Получение превью для главной страницы в формате "Организация - число стопок"
        public Task<List<StackPreview>> ListOrgStacks(string orgId); // Получение списка активных стопок организации
        #endregion

        #region Страница Друзья
        public Task<List<FriendsPreview>> GetFriendsPreview(IEnumerable<string> friendIds);  // Получение превью для страницы друзья в формате "vk id - число стопок"
        #endregion


        public Task<List<StackPreview>> ListUserStacks(string uid);
        public Task<List<StackPreview>> ListUserStacks(string uid, string token);
    }
}
