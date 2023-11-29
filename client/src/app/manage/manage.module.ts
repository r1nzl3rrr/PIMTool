import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ManageComponent } from './manage.component';
import { ManageRoutingModule } from './manage-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ManageComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    DatePipe,
    ManageRoutingModule
  ]
})
export class ManageModule { }
