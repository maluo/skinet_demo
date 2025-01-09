import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket-item';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  //would need to use public modifer if we are accessing observables
  constructor(public basketService:BasketService, public accountService : AccountService){

  }

  getCount(items: BasketItem[]){
    return items.reduce((sum,item) => sum + item.quantity, 0);
  }
}
