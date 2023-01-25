using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class Checkout : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }
        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
        public string PaymentAmount { get; set; }
        public string ErrorMessage { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection(HardCoded.UserId);
                if (ShoppingCartItems != null)
                {
                    decimal i = 0;
                    foreach (var item in ShoppingCartItems)
                        i += (item.Price * item.Qty);
                    PaymentAmount = i.ToString("C");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
