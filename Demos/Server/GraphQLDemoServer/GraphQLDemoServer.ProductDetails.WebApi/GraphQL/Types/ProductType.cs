﻿using GraphQLDemoServer.ProductDetails.WebApi.GraphQL.Types;
using GraphQLDemoServer.ProductDetails.WebApi.GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace GraphQLDemoServer.ProductDetails.WebApi.GraphQL.Types
{
    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor
                .Field(f => f.Brand)
                .ResolveWith<ProductBrandResolver>(b => b.GetBrand(default!, default!))
                .UseDbContext<AppDbContext>();
        }

        private class ProductBrandResolver
        {
            public Brand GetBrand(Product product, [ScopedService] AppDbContext context)
            {
                return context.Brands
                    .Where(p => p.Id == product.BrandId)
                    .FirstOrDefault();
            }
        }
    }
}