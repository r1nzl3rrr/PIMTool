import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ProjectComponent } from './project/project.component';
import { NewRoutingModule } from './new-routing.module';
import { CustomerComponent } from './customer/customer.component';
import { SupplierComponent } from './supplier/supplier.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ProjectComponent,
    CustomerComponent,
    SupplierComponent
  ],
  imports: [
    CommonModule,
    NewRoutingModule,
    DatePipe,
    SharedModule,
    FormsModule
  ],
  providers: [DatePipe]
})
export class NewModule { }
