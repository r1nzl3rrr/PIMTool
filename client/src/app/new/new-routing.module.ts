import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectComponent } from './project/project.component';
import { CustomerComponent } from './customer/customer.component';
import { SupplierComponent } from './supplier/supplier.component';

const routes: Routes = [
  {path: 'project', component: ProjectComponent},
  {path: 'customer', component: CustomerComponent},
  {path: 'supplier', component: SupplierComponent}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class NewRoutingModule { }
