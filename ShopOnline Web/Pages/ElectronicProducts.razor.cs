using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class ElectronicProducts : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductDto> Electronics { get; set; }
        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Electronics = await ProductService.GetItemsByCategory(1);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
