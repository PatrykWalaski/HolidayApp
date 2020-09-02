import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HolidaysComponent } from './holidays.component';
import { HolidaysOfferComponent } from './holidays-offer/holidays-offer.component';
import { HolidaysDetailComponent } from './holidays-detail/holidays-detail.component';
import { HolidaysRoutingModule } from './holidays-routing.module';
import { SharedModule } from '../shared/shared.module';
@NgModule({
  declarations: [HolidaysComponent, HolidaysOfferComponent, HolidaysDetailComponent],
  imports: [
    CommonModule,
    HolidaysRoutingModule,
    SharedModule
  ]
})
export class HolidaysModule { }
