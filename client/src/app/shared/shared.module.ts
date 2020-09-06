import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CarouselModule} from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {PaginationModule } from 'ngx-bootstrap/pagination';
import {PagerComponent} from './components/pager/pager.component';

@NgModule({
  declarations: [PagerComponent],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    PaginationModule,
    PagerComponent,
    CarouselModule,
    BsDropdownModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
