using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReitanDistribution.Core
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Unit))]
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }
        public int AmountInPackage { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int AmountInStock { get; set; }
        [ForeignKey(nameof(Supplier))]
        public Guid? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}