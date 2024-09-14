import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css']
})
export class SalesComponent implements OnInit {

  public orders!: Order[];
  private salesUrl = "http://localhost:8087/gateway/sales";
  //private salesUrl = "http://localhost:5041/gateway/sales";

  constructor(http: HttpClient) {
    http.get<Order[]>(this.salesUrl).subscribe(result => {
      this.orders = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

}

interface Order {
  OrderId: number;
  Description: string;
  Items: OrderItem[];
  Created: Date;
  Updated: Date;
}

interface OrderItem {
  OrderId: number;
  ProductId: number;
  Created: Date;
  Updated: Date;
}
