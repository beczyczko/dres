using System.Collections.Immutable;
using System.Text.Json;
using Dres.Core;
using Microsoft.AspNetCore.Mvc;

namespace Dres.Catwalk.Controllers
{
    [ApiController]
    [Route("api/puml")]
    public class PumlController : ControllerBase
    {
        private readonly ILogger<PumlController> _logger;
        private readonly IResourceRelationsPumlBuilder _resourceRelationsPumlBuilder;

        public PumlController(
            ILogger<PumlController> logger,
            IResourceRelationsPumlBuilder resourceRelationsPumlBuilder)
        {
            _logger = logger;
            _resourceRelationsPumlBuilder = resourceRelationsPumlBuilder;
        }

        [HttpGet("combined")]
        public string GetCombined([FromQuery] string[] specIds)
        {
            var resources = specIds.SelectMany(id => ResourcesById(id));
            var puml = _resourceRelationsPumlBuilder.Build(resources);
            return puml;
        }


        [HttpGet("{id}")]
        public string ById(string id)
        {
            return "";
        }

        private IImmutableList<Resource> ResourcesById(string id)
        {
          //todo db to database
            if (id == "1")
            {
                var resorucesJson = @"

[
  {
    ""name"": ""Country"",
    ""domainContext"": ""Resources.Countries"",
    ""identifier"": ""Resources.Countries.Country"",
    ""type"": ""Test.Resources.Countries.Country, Test.Resources.Countries, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Countries.Country"",
        ""name"": ""Id"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Countries.Country"",
        ""name"": ""Name"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      }
    ]
  },
  {
    ""name"": ""CreditCard"",
    ""domainContext"": ""Resources.Customers"",
    ""identifier"": ""Resources.Customers.CreditCard"",
    ""type"": ""Test.Resources.Customers.CreditCard, Test.Resources.Customers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Customers.CreditCard"",
        ""name"": ""Id"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.CreditCard"",
        ""name"": ""NameOnCard"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.CreditCard"",
        ""name"": ""CardNumber"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.CreditCard"",
        ""name"": ""Active"",
        ""type"": ""Boolean"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.CreditCard"",
        ""name"": ""Created"",
        ""type"": ""DateTime"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.CreditCard"",
        ""name"": ""Expiry"",
        ""type"": ""DateTime"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.CreditCard"",
        ""name"": ""Customer"",
        ""type"": ""Customer"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Customers.Customer""
        ]
      }
    ]
  },
  {
    ""name"": ""Customer"",
    ""domainContext"": ""Resources.Customers"",
    ""identifier"": ""Resources.Customers.Customer"",
    ""type"": ""Test.Resources.Customers.Customer, Test.Resources.Customers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Customers.Customer"",
        ""name"": ""Id"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.Customer"",
        ""name"": ""FirstName"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.Customer"",
        ""name"": ""LastName"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.Customer"",
        ""name"": ""Email"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.Customer"",
        ""name"": ""Password"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.Customer"",
        ""name"": ""Balance"",
        ""type"": ""Decimal"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Customers.Customer"",
        ""name"": ""CountryId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Countries.Country""
        ]
      }
    ]
  },
  {
    ""name"": ""Cart"",
    ""domainContext"": ""Resources.Carts"",
    ""identifier"": ""Resources.Carts.Cart"",
    ""type"": ""Test.Resources.Carts.Cart, Test.Resources.Carts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Carts.Cart"",
        ""name"": ""Id"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.Cart"",
        ""name"": ""Products"",
        ""type"": ""ReadOnlyCollection`1"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.Cart"",
        ""name"": ""CustomerId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Customers.Customer""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.Cart"",
        ""name"": ""TotalCost"",
        ""type"": ""Decimal"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.Cart"",
        ""name"": ""TotalTax"",
        ""type"": ""Decimal"",
        ""relatedResourcesIdentifiers"": []
      }
    ]
  },
  {
    ""name"": ""CartProduct"",
    ""domainContext"": ""Resources.Carts"",
    ""identifier"": ""Resources.Carts.CartProduct"",
    ""type"": ""Test.Resources.Carts.CartProduct, Test.Resources.Carts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Carts.CartProduct"",
        ""name"": ""CartId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Carts.Cart""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.CartProduct"",
        ""name"": ""CustomerId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Customers.Customer""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.CartProduct"",
        ""name"": ""Quantity"",
        ""type"": ""Int32"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.CartProduct"",
        ""name"": ""ProductId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Products.Product""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.CartProduct"",
        ""name"": ""Created"",
        ""type"": ""DateTime"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.CartProduct"",
        ""name"": ""Cost"",
        ""type"": ""Decimal"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Carts.CartProduct"",
        ""name"": ""Tax"",
        ""type"": ""Decimal"",
        ""relatedResourcesIdentifiers"": []
      }
    ]
  },
  {
    ""name"": ""Product"",
    ""domainContext"": ""Resources.Products"",
    ""identifier"": ""Resources.Products.Product"",
    ""type"": ""Test.Resources.Products.Product, Test.Resources.Products, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Id"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Name"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Created"",
        ""type"": ""DateTime"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Modified"",
        ""type"": ""DateTime"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Active"",
        ""type"": ""Boolean"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Quantity"",
        ""type"": ""Int32"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Cost"",
        ""type"": ""Decimal"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Code"",
        ""type"": ""ProductCode"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Products.ProductCode""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Product"",
        ""name"": ""Returns"",
        ""type"": ""ReadOnlyCollection`1"",
        ""relatedResourcesIdentifiers"": []
      }
    ]
  },
  {
    ""name"": ""ProductCode"",
    ""domainContext"": ""Resources.Products"",
    ""identifier"": ""Resources.Products.ProductCode"",
    ""type"": ""Test.Resources.Products.ProductCode, Test.Resources.Products, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Products.ProductCode"",
        ""name"": ""Id"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.ProductCode"",
        ""name"": ""Name"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      }
    ]
  },
  {
    ""name"": ""Return"",
    ""domainContext"": ""Resources.Products"",
    ""identifier"": ""Resources.Products.Return"",
    ""type"": ""Test.Resources.Products.Return, Test.Resources.Products, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Products.Return"",
        ""name"": ""Product"",
        ""type"": ""Product"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Products.Product""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Return"",
        ""name"": ""CustomerId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Customers.Customer""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Return"",
        ""name"": ""Reason"",
        ""type"": ""ReturnReason"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Return"",
        ""name"": ""Created"",
        ""type"": ""DateTime"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Products.Return"",
        ""name"": ""Note"",
        ""type"": ""String"",
        ""relatedResourcesIdentifiers"": []
      }
    ]
  },
  {
    ""name"": ""Purchase"",
    ""domainContext"": ""Resources.Purchases"",
    ""identifier"": ""Resources.Purchases.Purchase"",
    ""type"": ""Test.Resources.Purchases.Purchase, Test.Resources.Purchases, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Purchases.Purchase"",
        ""name"": ""Id"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Purchases.Purchase"",
        ""name"": ""Products"",
        ""type"": ""ReadOnlyCollection`1"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Purchases.Purchase"",
        ""name"": ""Created"",
        ""type"": ""DateTime"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Purchases.Purchase"",
        ""name"": ""CustomerId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Customers.Customer""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Purchases.Purchase"",
        ""name"": ""TotalTax"",
        ""type"": ""Decimal"",
        ""relatedResourcesIdentifiers"": []
      },
      {
        ""resourceIdentifier"": ""Resources.Purchases.Purchase"",
        ""name"": ""TotalCost"",
        ""type"": ""Decimal"",
        ""relatedResourcesIdentifiers"": []
      }
    ]
  },
  {
    ""name"": ""PurchasedProduct"",
    ""domainContext"": ""Resources.Purchases"",
    ""identifier"": ""Resources.Purchases.PurchasedProduct"",
    ""type"": ""Test.Resources.Purchases.PurchasedProduct, Test.Resources.Purchases, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""properties"": [
      {
        ""resourceIdentifier"": ""Resources.Purchases.PurchasedProduct"",
        ""name"": ""PurchaseId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Purchases.Purchase""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Purchases.PurchasedProduct"",
        ""name"": ""ProductId"",
        ""type"": ""Guid"",
        ""relatedResourcesIdentifiers"": [
          ""Resources.Products.Product""
        ]
      },
      {
        ""resourceIdentifier"": ""Resources.Purchases.PurchasedProduct"",
        ""name"": ""Quantity"",
        ""type"": ""Int32"",
        ""relatedResourcesIdentifiers"": []
      }
    ]
  }
]              
";
                var resources = JsonSerializer.Deserialize<Resource[]>(resorucesJson, options: new JsonSerializerOptions()
                {
                  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    
                })!.ToImmutableList();
                return resources;
            }

            if (id == "2")
            {
                return JsonSerializer.Deserialize<Resource[]>("[]")!.ToImmutableList();
            }

            return Array.Empty<Resource>().ToImmutableList();
        }
    }
}