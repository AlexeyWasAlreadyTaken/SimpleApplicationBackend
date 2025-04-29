using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApplicationBack.DTO;
using SimpleApplicationBack.Interfaces;
using SimpleApplicationBack.Models;

namespace SimpleApplicationBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDTO>>(_productRepository.GetProducts());
            var colorsList = _productRepository.GetColors();
            var typesList = _productRepository.GetTypes();

            foreach (var product in products)
            {
                product.ColorCode = colorsList.Where(i => i.Id == product.ColorId).FirstOrDefault().Code.ToString();
                product.TypeName = typesList.Where(i => i.Id == product.TypeId).FirstOrDefault().Name.ToString();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(Guid productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _mapper.Map<ProductDTO>(_productRepository.GetProduct(productId));

            if (!_productRepository.ColorExists(product.ColorId))
                return NotFound();
            var color = _productRepository.GetProductColor(product.ColorId);
            product.ColorCode = color;

            if (!_productRepository.TypeExists(product.TypeId))
                return NotFound();
            var type = _productRepository.GetProductType(product.TypeId);
            product.TypeName = type;



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.ColorExists(productDto.ColorId))
                return BadRequest("Specified color id does not contains in data base. Verify Color ID in db");

            if (!_productRepository.TypeExists(productDto.TypeId))
                return BadRequest("Specified product type id does not contains in data base. Verify Type ID in db");

            var product = _mapper.Map<Product>(productDto);
            _productRepository.CreateProduct(product);

            return Ok(product);
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(Guid id, [FromBody] ProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is null");

            if (id != productDto.Id)
                return BadRequest("different ids");

            if (!_productRepository.ProductExists(id))
                return BadRequest("Specified product not exists in db");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(productDto);

            if (!_productRepository.UpdateProduct(product))
            {
                ModelState.AddModelError("", "Somthing went wrong updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(Guid productId) 
        {
            if(!_productRepository.ProductExists(productId))
                return NotFound();

            var productToDelete = _productRepository.GetProduct(productId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }

            return NoContent();
        }


    }
}
