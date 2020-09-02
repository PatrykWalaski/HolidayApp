import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HolidaysComponent } from './holidays.component';
import { HolidaysDetailComponent } from './holidays-detail/holidays-detail.component';


const routes: Routes = [
  {path: '', component: HolidaysComponent},
  {path: ':id', component: HolidaysDetailComponent}
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
