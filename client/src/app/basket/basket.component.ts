import { Component } from '@angular/core';
import { BasketItem } from '../shared/models/basket-item';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent {
  constructor(public basketService: BasketService) { }

  increamentQuantity(item: BasketItem) {
    this.basketService.addItemToBasket(item);
  }
  removeItem(event: {id: number, quantity: number}) {
    this.basketService.removeItemFromBasket(event.id, event.quantity);
  }
}