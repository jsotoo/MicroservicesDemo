﻿using Microservices.Infrastructure.Crosscutting.Util;
using Microservices.Infrastructure.MessageBus;
using Microservices.Products.Infrastructure.Dto;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Microservices.Products.ReadModels.Client
{
    public class ProductsView : IProductsView
    {
        private const string ProductsServiceUrl = "https://localhost:44339/api/";

        private static ConcurrentDictionary<Guid, ProductDto> products = new ConcurrentDictionary<Guid, ProductDto>();

        static ProductsView()
        {
            InitializeProducts();

            var ProductsEventListener = new TransientSubscriber(
                "Products_client_productview_" + Assembly.GetEntryAssembly().FullName.Split(',').FirstOrDefault(),
                "Microservices.Products.Infrastructure.Events",
                () =>
                {
                    ResetProducts();
                    InitializeProducts();
                });
        }

        public void Initialize()
        {
            InitializeProducts();
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            return products.Values.AsEnumerable();
        }

        public ProductDto GetById(Guid id)
        {
            ProductDto result;
            if (products.TryGetValue(id, out result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("Cannot find the product.");
            }
        }

        public void UpdateLocalCache(ProductDto newValue)
        {
            ProductDto retrievedValue;
            Guid searchKey = newValue.Id;

            if (products.TryGetValue(searchKey, out retrievedValue))
            {
                if (!products.TryUpdate(searchKey, newValue, retrievedValue))
                {
                    throw new ApplicationException("Failed to update the product in the cache");
                }
            }
            else
            {
                if (!products.TryAdd(searchKey, newValue))
                {
                    throw new ApplicationException("Failed to add new product to the cache");
                }
            }
        }

        public void Reset()
        {
            ResetProducts();
        }

        private static void InitializeProducts()
        {
            var result = Try.To(LoadProducts)
                .OnFailedAttempt(() => Thread.Sleep(1000))
                .UpTo(3)
                .Times();

            if (!result.Succeeded)
                throw new ApplicationException("Failed to load the Products from the Products service.", result.Exception);
        }

        private static void LoadProducts()
        {
            var apiClient = new ApiClient();
            var apiResponse = apiClient.LoadMany<ProductDto>(ProductsServiceUrl + "products");

            foreach (var product in apiResponse)
                products.TryAdd(product.Id, product);
        }

        private static void ResetProducts()
        {
            products = new ConcurrentDictionary<Guid, ProductDto>();
        }

    }
}
