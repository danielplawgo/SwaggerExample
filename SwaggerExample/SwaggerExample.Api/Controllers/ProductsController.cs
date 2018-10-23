using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bogus;
using SwaggerExample.Api.Models;

namespace SwaggerExample.Api.Controllers
{
    /// <summary>
    /// The products API.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class ProductsController : ApiController
    {
        private static List<Product> _products;

        static ProductsController()
        {
            int id = 1;
            _products = new Faker<Product>()
                .RuleFor(o => o.Id, f => id++)
                .RuleFor(p => p.Name, (f, p) => f.Commerce.ProductName())
                .RuleFor(p => p.Category, (f, p) => f.Commerce.Categories(1).FirstOrDefault())
                .RuleFor(p => p.Price, (f, p) => f.Random.Number(100, 100000) / 100M)
                .Generate(10);
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> Get()
        {
            return _products;
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Product Get(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Add the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Post([FromBody]Product product)
        {
            product.Id = _products.Select(p => p.Id).Max() + 1;
            _products.Add(product);
        }

        /// <summary>
        /// Update the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="product">The product.</param>
        public void Put(int id, [FromBody]Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);

            if (existingProduct != null)
            {
                existingProduct.Category = product.Category;
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
            }
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}
