import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './components/pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TextInputComponent } from './components/text-input/text-input.component';
import { AlertModule } from 'ngx-bootstrap/alert';
import { TagInputModule } from 'ngx-chips';




@NgModule({
  declarations: [
    PagerComponent,
    TextInputComponent,
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    BsDatepickerModule,
    ReactiveFormsModule,
    FormsModule,
    TagInputModule,
    AlertModule
  ],
  exports: [
    PaginationModule,
    BsDatepickerModule,
    PagerComponent,
    ReactiveFormsModule,
    TextInputComponent,
    AlertModule,
    TagInputModule
  ],
})
export class SharedModule { }
