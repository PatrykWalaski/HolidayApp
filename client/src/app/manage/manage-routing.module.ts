import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ManageComponent } from './manage.component';
import { ManageHolidayComponent } from './manage-holiday/manage-holiday.component';

const routes: Routes = [
  {path: '', component: ManageComponent},
  {path: 'holiday/:id', component: ManageHolidayComponent}
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
export class ManageRoutingModule { }
