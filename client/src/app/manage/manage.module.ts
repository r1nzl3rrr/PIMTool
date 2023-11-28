import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage.component';
import { ManageRoutingModule } from './manage-routing.module';
import { ProjectItemComponent } from './project-item/project-item.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ManageComponent,
    ProjectItemComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ManageRoutingModule
  ]
})
export class ManageModule { }
