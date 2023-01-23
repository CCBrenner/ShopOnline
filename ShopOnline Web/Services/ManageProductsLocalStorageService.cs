using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        public ManageProductsLocalStorageService(IProductService productService, ILocalStorageService localStorageService)
        {
            ProductService = productService;
            LocalStorageService = localStorageService;
        }

        public IProductService ProductService { get; }
        public ILocalStorageService LocalStorageService { get; }
        private string key = "ProductCollection";

        public async Task<IEnumerable<ProductDto>> GetCollection() =>
            await LocalStorageService.GetItemAsync<IEnumerable<ProductDto>>(key)
                ?? await AddCollection();

        public async Task RemoveCollection() =>
            await LocalStorageService.RemoveItemAsync(key);

        private Task<IEnumerable<ProductDto>> AddCollection() =>
            ProductService.GetItems();
    }
}
