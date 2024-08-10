using Microservices.Products.Infrastructure.Dto;
using Microservices.Products.ReadModels.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Microservices.Sales.ReadModels.API.Views
{
    public class ProductView
    {
        private readonly IProductsMaterializedView _productsMaterializedView;
        
        public ProductView(IProductsMaterializedView productsMaterializedView)
        {
            _productsMaterializedView = productsMaterializedView;            
        }

        public ProductView() : this(new ProductsMaterializedView("Sales.Read.Notifications")) { }

        public IEnumerable<ProductDto> GetAll()
        {
            return _productsMaterializedView.GetProducts().Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                DisplayName = product.DisplayName,
                Price = product.Price
            }); 
        }
        

        public ProductDto GetById(Guid id)
        {
            var product = _productsMaterializedView.GetById(id);
            if (product != null)
            {
                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    DisplayName = product.DisplayName,
                    Price = product.Price
                };
            }            

            throw new ArgumentOutOfRangeException("id","A product with the id of " + id.ToString() + "couldn't be found");
        }
    }
}
