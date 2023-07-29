using Stacks_rework.Models;
using Stacks_rework.Models.Output;

namespace Stacks_rework.Services
{
    public interface IStacksService
    {
        #region Основной функционал
        public Task<UserStack> Create(UserStack userStack);
        public Task<AdvertisementStack> CreateAdvertisementStack(AdvertisementStack adStack, string companyToken);

        public Task<UserStack> GetUserStack(string id, string uid, string token);
        public Task<AdvertisementStack> GetAdStack(string id);

        public Task<UserStack> UpdateUserStack(string id, string ownerid, string token, UpdateStack newStack);
        public Task<AdvertisementStack> UpdateOrgStack(string id, string ownerid, string token, UpdateStack newStack);

        public Task DropUserStack(string id, string ownerid, string token);
        #endregion

        #region Главная Страница
        public Task<List<StackPreview>> GetAdsPreview(); // Получение превью для главной страницы в формате "Организация - число стопок"
        public Task<List<StackPreview>> ListOrgStacks(string orgId); // Получение списка активных стопок организации
        #endregion

        #region Страница Друзья
        public Task<List<FriendsPreview>> GetFriendsPreview(IEnumerable<string> friendIds);  // Получение превью для страницы друзья в формате "vk id - число стопок"
        public Task<List<StackPreview>> ListFriendStacks(string uid); // Получение списка публичных стопок друга
        #endregion

        #region Профиль пользователя
        public Task<List<StackPreview>> ListPersonalStacks(string token, string uid); // Получение полного списка стопок пользователя
        #endregion
    }
}
