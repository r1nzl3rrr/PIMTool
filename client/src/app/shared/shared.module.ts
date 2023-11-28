import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';



@NgModule({
  declarations: [
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule
  ],
  exports: [
    PaginationModule,
    PagerComponent
  ]
})
export class SharedModule { }
