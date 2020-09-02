import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage.component';
import { ManageRoutingModule } from '../manage/manage-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    ManageRoutingModule,
    SharedModule
  ],
  declarations: [ManageComponent]
})
export class ManageModule { }
