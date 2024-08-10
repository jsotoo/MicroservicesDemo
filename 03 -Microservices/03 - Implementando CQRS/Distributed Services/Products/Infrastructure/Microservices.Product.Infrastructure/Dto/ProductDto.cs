
using Microservices.Infrastructure.Crosscutting;

namespace Microservices.Products.Infrastructure.Dto
{
    public class ProductDto : ReadObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Version { get; set; }
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return $"Product ID: {Id}, Name: {Name}, Description: {Description}, Price: {Price}";
        }
    }
}

