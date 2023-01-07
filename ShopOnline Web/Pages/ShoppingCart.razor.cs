using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class ShoppingCart : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public IJSRuntime Js { get; set; }
        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; set; }
        public string TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        protected async override Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CartChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        protected async void UpdateQty_Input(int id) =>
            MakeQtyUpdateButtonVisible(id, true);
        private async Task MakeQtyUpdateButtonVisible(int id, bool visible) =>
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        protected async void UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty,
                    };
                    if (ShoppingCartService.UpdateQty(updateItemDto) != null)
                    {
                        UpdateItemTotalPrice(id);
                        CartChanged();
                        MakeQtyUpdateButtonVisible(id, false);
                    }
                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                    if (item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void DeleteCartItem_Click(int id)
        {
            if (ShoppingCartService.DeleteItem(id) != null)
                RemoveCartItem(id);
            CartChanged();
        }
        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);
            List<CartItemDto> cartItemsList = ShoppingCartItems.ToList();
            cartItemsList.Remove(cartItemDto);
            ShoppingCartItems = cartItemsList;
        }
        private CartItemDto GetCartItem(int id) =>
            ShoppingCartItems.FirstOrDefault(x => x.Id == id);

        private void CalculateCartSumaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }
        private void SetTotalPrice() => 
            TotalPrice = ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
        private void SetTotalQuantity() => 
            TotalQuantity = ShoppingCartItems.Sum(p => p.Qty);

        private void UpdateItemTotalPrice(int id)
        {
            var item = GetCartItem(id);

            if (item != null)
                item.TotalPrice = item.Price * item.Qty;
        }
        private void CartChanged()
        {
            CalculateCartSumaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
    }
}
