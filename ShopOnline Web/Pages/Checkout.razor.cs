using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class Checkout : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public IJSRuntime Js { get; set; }
        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
        public string PaymentAmount { get; set; }
        public string ErrorMessage { get; set; }

        protected async override void OnInitialized()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
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
        /*
        protected async void Pay_Click() =>
            await Js.InvokeVoidAsync("PurchaseShoppingCartItems", true);*/
    }
}
