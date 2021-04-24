using System;

namespace MySecondAPI.Model
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public Guid ProductCategoryId { get; set; }
        public int ProductPrice { get; set; }
        public Guid ProductSupplierId { get; set; }
        public int ProductAmount { get; set; }
        public string ProductColor { get; set; }
        public string ProductMeterial { get; set; }
    }
}