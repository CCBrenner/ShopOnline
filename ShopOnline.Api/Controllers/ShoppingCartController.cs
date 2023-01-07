using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Api.Respositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,
                                    IProductRepository productRepository)
        {
            ShoppingCartRepository = shoppingCartRepository;
            ProductRepository = productRepository;
        }

        public IShoppingCartRepository ShoppingCartRepository { get; }
        public IProductRepository ProductRepository { get; }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await this.ShoppingCartRepository.GetItems(userId);

                if (cartItems == null)
                {
                    return NoContent();
                }

                var products = await this.ProductRepository.GetItems();

                if (products == null)
                {
                    throw new Exception("No product exists in the system");
                }
                var cartItemsDto = cartItems.ConvertToDto(products);

                return Ok(cartItemsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {
            try
            {
                var cartItem = await this.ShoppingCartRepository.GetItem(id);
                if (cartItem == null)
                {
                    return NotFound();
                }
                var product = await ProductRepository.GetItem(cartItem.ProductId);

                if (product == null)
                {
                    return NotFound();
                }
                var cartItemDto = cartItem.ConvertToDto(product);

                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var newCartItem = await this.ShoppingCartRepository.AddItem(cartItemToAddDto);

                if (newCartItem == null)
                {
                    return NoContent();
                }

                var product = await ProductRepository.GetItem(newCartItem.ProductId);

                if (product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({cartItemToAddDto.ProductId})");
                }

                var newCartItemDto = newCartItem.ConvertToDto(product);

                // this action method returns location of the added resource in
                // a response, which is standard procedure w/POST methods
                return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
                var cartItem = await this.ShoppingCartRepository.DeleteItem(id);
                if (cartItem == null) return NotFound();

                var product = await this.ProductRepository.GetItem(cartItem.ProductId);
                if (product == null) return NotFound();

                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartItemDto>> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var cartItem = await this.ShoppingCartRepository.UpdateQty(id, cartItemQtyUpdateDto);
                if (cartItem == null) return NotFound();

                var product = await this.ProductRepository.GetItem(id);
                var cartItemDto = cartItem.ConvertToDto(product);

                return Ok(cartItemDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
