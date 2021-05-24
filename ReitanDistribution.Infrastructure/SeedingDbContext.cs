using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Bogus;
using ReitanDistribution.Core;

namespace ReitanDistribution.Infrastructure
{
    public static class SeedingDbContext
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(category => category.Id, faker => faker.IndexGlobal + 1)
                .RuleFor(category => category.Name, faker => faker.Commerce.ProductMaterial())
                .RuleFor(category => category.Description, faker => faker.Commerce.ProductAdjective());

            var categories = categoryFaker.Generate(2);
            modelBuilder.Entity<Category>().HasData(categories);

            var units = new List<Unit>
            {
                new()
                {
                    Id = 1,
                    Name = "Kilogram"
                },
                new()
                {
                    Id = 2,
                    Name = "Styk"
                },
            };
            modelBuilder.Entity<Unit>().HasData(units);

            var supplierFaker = new Faker<Supplier>()
                .RuleFor(supplier => supplier.Id, Guid.NewGuid)
                .RuleFor(supplier => supplier.Name, faker => faker.Company.CompanyName())
                .RuleFor(supplier => supplier.Address, faker => faker.Address.FullAddress())
                .RuleFor(supplier => supplier.ZipCode, faker => faker.Address.ZipCode())
                .RuleFor(supplier => supplier.Mail, faker => faker.Person.Email)
                .RuleFor(supplier => supplier.ContactPerson, faker => faker.Person.FullName)
                .RuleFor(supplier => supplier.PhoneNumber, faker => faker.Person.Phone);

            var suppliers = supplierFaker.Generate(2);
            modelBuilder.Entity<Supplier>().HasData(suppliers);

            var productFaker = new Faker<Product>()
                .RuleFor(product => product.Id, Guid.NewGuid)
                .RuleFor(product => product.Name, faker => faker.Commerce.ProductName())
                .RuleFor(product => product.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(product => product.AmountInPackage, faker => faker.Random.Int(0, 50))
                .RuleFor(product => product.AmountInStock, faker => faker.Random.Int(0, 999))
                .RuleFor(product => product.Price, faker => decimal.Parse(faker.Commerce.Price()))
                .RuleFor(product => product.UnitId, faker => faker.PickRandom(units).Id)
                .RuleFor(product => product.CategoryId, faker => faker.PickRandom(categories).Id)
                .RuleFor(product => product.SupplierId, faker => faker.PickRandom(suppliers).Id);

            var products = productFaker.Generate(5);
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
