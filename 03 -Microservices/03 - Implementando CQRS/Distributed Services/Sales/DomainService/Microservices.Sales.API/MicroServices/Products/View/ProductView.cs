﻿using Microservices.Products.ReadModels.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Microservices.Sales.API.MicroServices.Products.View
{
    public class ProductView
    {
        private readonly IProductsMaterializedView _productsMaterializedView;
        
        public ProductView(IProductsMaterializedView productsMaterializedView)
        {
            _productsMaterializedView = productsMaterializedView;            
        }

        public ProductView() : this(new ProductsMaterializedView("Sales.Write.Notifications")) { }

        public IEnumerable<Domain.Product> GetAll()
        {
            return _productsMaterializedView.GetProducts().Select(p => new Domain.Product
            {
                Id = p.Id,
                Price = p.Price
            }); 
        }
        

        public Domain.Product GetById(Guid id)
        {
            var product = _productsMaterializedView.GetById(id);
            if (product != null)
            {
                return new Domain.Product
                {
                    Id = product.Id,
                    Price = product.Price
                };
            }            

            throw new ArgumentOutOfRangeException("id","A product with the id of " + id.ToString() + "couldn't be found");
        }
    }
}
