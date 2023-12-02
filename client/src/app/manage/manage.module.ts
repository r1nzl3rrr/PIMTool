import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage.component';
import { ManageRoutingModule } from './manage-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ProjectEditComponent } from './project-edit/project-edit.component';



@NgModule({
  declarations: [
    ManageComponent,
    ProjectEditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ManageRoutingModule
  ]
})
export class ManageModule { }
