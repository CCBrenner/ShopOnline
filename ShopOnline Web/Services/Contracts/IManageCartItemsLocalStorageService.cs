using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IManageCartItemsLocalStorageService
    {
        Task<IEnumerable<CartItemDto>> GetCollection(int id);
        Task RemoveCollection();
        Task SaveCollection(IEnumerable<CartItemDto> cartItemDtos);
    }
}
