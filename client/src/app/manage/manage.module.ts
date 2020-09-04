import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage.component';
import { ManageRoutingModule } from '../manage/manage-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ManageHolidayComponent } from './manage-holiday/manage-holiday.component';

@NgModule({
  imports: [
    CommonModule,
    ManageRoutingModule,
    SharedModule
  ],
  declarations: [ManageComponent, ManageHolidayComponent]
})
export class ManageModule { }
