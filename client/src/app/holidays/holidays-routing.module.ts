import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HolidaysComponent } from './holidays.component';
// import { ProductDetailsComponent } from './product-details/product-details.component';


const routes: Routes = [
  {path: '', component: HolidaysComponent},
  // {path: ':id', component: ProductDetailsComponent, data: {breadcrumb: {alias: 'productDetails'}}},
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule,
  ]
})
export class HolidaysRoutingModule { }
