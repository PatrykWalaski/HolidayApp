import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HolidaysComponent } from './holidays/holidays.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'holidays', loadChildren: () => import('./holidays/holidays.module').then(mod => mod.HolidaysModule) },
  { path: 'manage', loadChildren: () => import('./manage/manage.module').then(mod => mod.ManageModule) },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
