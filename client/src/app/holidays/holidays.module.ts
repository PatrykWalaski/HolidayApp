import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HolidaysComponent } from './holidays.component';
import { HolidaysRoutingModule } from './holidays-routing.module';
import { SharedModule } from '../shared/shared.module';
@NgModule({
  declarations: [HolidaysComponent],
  imports: [
    CommonModule,
    HolidaysRoutingModule,
    SharedModule
  ]
})
export class HolidaysModule { }
