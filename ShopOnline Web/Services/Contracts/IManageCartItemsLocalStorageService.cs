using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IManageCartItemsLocalStorageService
    {
        Task<IEnumerable<CartItemDto>> GetCollection();
        Task RemoveCollection();
        Task SaveCollection(IEnumerable<CartItemDtos> cartItemDtos);
    }
}
