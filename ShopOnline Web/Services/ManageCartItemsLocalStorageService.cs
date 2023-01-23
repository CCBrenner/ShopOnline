using Blazored.LocalStorage;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        public ManageCartItemsLocalStorageService(IShoppingCartService shoppingCartService, ILocalStorageService localStorageService)
        {
            ShoppingCartService = shoppingCartService;
            LocalStorageService = localStorageService;
        }

        public IShoppingCartService ShoppingCartService { get; }
        public ILocalStorageService LocalStorageService { get; }
        private string key = "CartItemCollection";

        public async Task<IEnumerable<CartItemDto>> GetCollection() =>
            await LocalStorageService.GetItemAsync<IEnumerable<CartItemDto>>(key)
                ?? await AddCollection(HardCoded.UserId);

        public async Task RemoveCollection() =>
            await LocalStorageService.RemoveItemAsync(key);

        public async Task SaveCollection(IEnumerable<CartItemDtos> cartItemDtos) =>
            await LocalStorageService.SetItemAsync(key, cartItemDtos);

        private async Task<IEnumerable<CartItemDto>> AddCollection(int userId) =>
           await ShoppingCartService.GetItems(userId);
    }
}
