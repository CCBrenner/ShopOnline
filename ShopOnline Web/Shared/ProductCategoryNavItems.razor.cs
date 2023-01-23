using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Shared
{
    public partial class ProductCategoryNavItems : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
        public string ErrorMessage { get; set; }

        protected async Task OnInitializedAsyc()
        {
            try
            {
                ProductCategories = await ProductService.GetCategories();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
