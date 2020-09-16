import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CarouselModule} from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {PaginationModule } from 'ngx-bootstrap/pagination';
import {PagerComponent} from './components/pager/pager.component';
import { NgxGalleryModule } from 'ngx-gallery-9';

@NgModule({
  declarations: [PagerComponent],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    NgxGalleryModule
  ],
  exports: [
    PaginationModule,
    PagerComponent,
    CarouselModule,
    BsDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    NgxGalleryModule
  ]
})
export class SharedModule { }
