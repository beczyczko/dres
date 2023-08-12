import {Component, OnInit} from '@angular/core';
import {BehaviorSubject} from "rxjs";

declare var plantuml: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'dres-catwalk';
  plantumlInitialized = new BehaviorSubject(false);

  public ngOnInit() {
    plantuml.initialize('assets/@sakirtemel/plantuml.js').then(() => {
      this.plantumlInitialized.next(true);

      // todo db render on some UI interaction
      const pumlContent = this.getPumlContent();
      this.renderPuml(pumlContent);
    })
  }

  public getPumlContent(): string {
    // todo db from API
    return `
@startuml

left to right direction
title Domain model relationships

frame Resources.Countries {
    class Country {
        + Id : Guid
        + Name : String
    }
}

frame Resources.Customers {
    class CreditCard {
        + Id : Guid
        + NameOnCard : String
        + CardNumber : String
        + Active : Boolean
        + Created : DateTime
        + Expiry : DateTime
        + Customer : Customer
    }
    class Customer {
        + Id : Guid
        + FirstName : String
        + LastName : String
        + Email : String
        + Password : String
        + Balance : Decimal
        + CountryId : Guid
    }
}

frame Resources.Carts {
    class Cart {
        + Id : Guid
        + Products : ReadOnlyCollection
        + CustomerId : Guid
        + TotalCost : Decimal
        + TotalTax : Decimal
    }
    class CartProduct {
        + CartId : Guid
        + CustomerId : Guid
        + Quantity : Int32
        + ProductId : Guid
        + Created : DateTime
        + Cost : Decimal
        + Tax : Decimal
    }
}

frame Resources.Products {
    class Product {
        + Id : Guid
        + Name : String
        + Created : DateTime
        + Modified : DateTime
        + Active : Boolean
        + Quantity : Int32
        + Cost : Decimal
        + Code : ProductCode
        + Returns : ReadOnlyCollection
    }
    class ProductCode {
        + Id : Guid
        + Name : String
    }
    class Return {
        + Product : Product
        + CustomerId : Guid
        + Reason : ReturnReason
        + Created : DateTime
        + Note : String
    }
}

frame Resources.Purchases {
    class Purchase {
        + Id : Guid
        + Products : ReadOnlyCollection
        + Created : DateTime
        + CustomerId : Guid
        + TotalTax : Decimal
        + TotalCost : Decimal
    }
    class PurchasedProduct {
        + PurchaseId : Guid
        + ProductId : Guid
        + Quantity : Int32
    }
}



Resources.Customers.CreditCard "Customer" ...> "Id" Resources.Customers.Customer
Resources.Customers.Customer "CountryId" ...> "Id" Resources.Countries.Country
Resources.Customers.Customer "CountryId" ...> "Id" SecondSystem.Resources.Countries.Country
Resources.Carts.Cart "CustomerId" ...> "Id" Resources.Customers.Customer
Resources.Carts.CartProduct "CartId" ...> "Id" Resources.Carts.Cart
Resources.Carts.CartProduct "CustomerId" ...> "Id" Resources.Customers.Customer
Resources.Carts.CartProduct "ProductId" ...> "Id" Resources.Products.Product
Resources.Products.Product "Code" ...> "Id" Resources.Products.ProductCode
Resources.Products.Return "Product" ...> "Id" Resources.Products.Product
Resources.Products.Return "CustomerId" ...> "Id" Resources.Customers.Customer
Resources.Purchases.Purchase "CustomerId" ...> "Id" Resources.Customers.Customer
Resources.Purchases.PurchasedProduct "PurchaseId" ...> "Id" Resources.Purchases.Purchase
Resources.Purchases.PurchasedProduct "ProductId" ...> "Id" Resources.Products.Product

@enduml
              `;
  }

  public renderPuml(pumlContent: string): Promise<any> {
    return plantuml.renderPng(pumlContent)
      .then((blob: any) => {
        this.plantUmlHtmlElement.src = window.URL.createObjectURL(blob)
      });
  }

  public get plantUmlHtmlElement(): any {
    return document.getElementById('plantuml-diagram');
  }
}
