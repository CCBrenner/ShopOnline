using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetItem(int id);
        Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId);
        Task<IEnumerable<ProductCategoryDto>> GetCategories();
    }
}
