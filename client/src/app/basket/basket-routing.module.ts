import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketComponent } from './basket.component';
import { Routes, RouterModule } from '@angular/router';
import { CheckoutRoutingModule } from '../checkout/checkout-routing.module';

const routes: Routes = [{ path: '', component: BasketComponent }];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BasketRoutingModule {}
