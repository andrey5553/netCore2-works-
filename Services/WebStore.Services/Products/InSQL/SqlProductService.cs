using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSQL
{
    public class SqlProductData : IProductService
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<SectionDTO> GetCategories() => _db.Sections.ToDTO();

        public IEnumerable<BrandDTO> GetBrands() => _db.Brands.ToDTO();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products
               .Include(product => product.Brand)
               .Include(product => product.Category);

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.BrandId != null)
                    query = query.Where(product => product.BrandId == Filter.BrandId);

                if (Filter?.SectionId != null)
                    query = query.Where(product => product.CategoryId == Filter.SectionId);
            }

            return query.AsEnumerable().ToDTO();
        }

        public ProductDTO GetProductById(int id) => _db.Products
           .Include(product => product.Brand)
           .Include(product => product.Category)
           .FirstOrDefault(product => product.Id == id)
           .ToDTO();
    }
}