using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductService
    {
        private readonly IProductService _productService;

        public ProductsApiController(IProductService productService) => _productService = productService;

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetCategories()
        {
            return _productService.GetCategories();
        }

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands()
        {
            return _productService.GetBrands();
        }

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter = null)
        {
            return _productService.GetProducts(Filter);
        }

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }
    }
}