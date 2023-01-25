﻿using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class Products : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }
        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }
        public IEnumerable<ProductDto> ProductsList { get; set; }
        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();
                ProductsList = await ManageProductsLocalStorageService.GetCollection();
                var shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection(HardCoded.UserId);

                var totalQty = shoppingCartItems.Sum(i => i.Qty);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory() =>
            from product in ProductsList
            group product by product.CategoryId into prodByCatGroup
            orderby prodByCatGroup.Key
            select prodByCatGroup;
        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos) =>
            groupedProductDtos.FirstOrDefault(prodGroup => prodGroup.CategoryId == groupedProductDtos.Key).CategoryName;
        private async Task ClearLocalStorage()
        {
            await ManageProductsLocalStorageService.RemoveCollection();
            await ManageCartItemsLocalStorageService.RemoveCollection();
        }
    }
}
