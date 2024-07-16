using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core;
using OrderSystem.Core.Entities;
using OrderSystem.PL.DTOs;

namespace OrderSystem.PL.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(NotFound), 404)]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<IReadOnlyList<Product>?>> GetAllProducts()
        {
            var Products = await _unitOfWork.Repository<Product>().GetAllAsync();
            if (Products == null)
                return NotFound("No Products Yet");
            return Ok(Products);
        }

        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(NotFound), 404)]
        [HttpGet("{productId}")]
        public async Task<ActionResult<Product?>> GetProductById(int productId)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
            if (product == null)
                return NotFound("Product Not Found");
            return Ok(product);
        }



        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductDto ProductParams)
        {
            var Product = new Product()
            {
                Name = ProductParams.Name,
                Price = ProductParams.Price,
                Stock = ProductParams.Stock
            };
            await _unitOfWork.Repository<Product>().AddAsync(Product);
            if (await _unitOfWork.CompleteAsync() > 0)
                return Ok(Product);
            return BadRequest("Failed To Add This Product.. Try Again");
        }


        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(NotFound), 404)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}")]
        public async Task<ActionResult<Product>> UpdateProduct(int productId, ProductDto ProductParams)
        {
            var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
            if (Product == null)
                return NotFound("Product Not Found");
            Product.Name = ProductParams.Name;
            Product.Price = ProductParams.Price;
            Product.Stock = ProductParams.Stock;
            if (await _unitOfWork.CompleteAsync() > 0)
                return Ok(Product);
            return BadRequest("Failed To Update This Product.. Try Again");
        }
    }
}
