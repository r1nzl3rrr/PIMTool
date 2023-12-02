import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';





@NgModule({
  declarations: [
    PagerComponent,
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    PaginationModule,
    BsDatepickerModule,
    PagerComponent,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
