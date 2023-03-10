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

        public async Task<IEnumerable<CartItemDto>> GetCollection(int id) =>
            await LocalStorageService.GetItemAsync<IEnumerable<CartItemDto>>(key)  // Get item from local storage
                ?? await AddCollection(id);  // If null, Add item from DB

        public async Task RemoveCollection() =>
            await LocalStorageService.RemoveItemAsync(key);

        public async Task SaveCollection(IEnumerable<CartItemDto> cartItemDtos) =>
            await LocalStorageService.SetItemAsync(key, cartItemDtos);

        private async Task<IEnumerable<CartItemDto>> AddCollection(int id)
        {
            var shoppingCartCollection = await ShoppingCartService.GetItems(id);
            if (shoppingCartCollection != null)
                await SaveCollection(shoppingCartCollection);
            return shoppingCartCollection;
        }
    }
}
