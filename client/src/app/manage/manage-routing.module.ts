import { NgModule } from '@angular/core';
import { ManageComponent } from './manage.component';
import { RouterModule, Routes } from '@angular/router';
import { ProjectEditComponent } from './project-edit/project-edit.component';

const routes : Routes = [
  {path: '', component: ManageComponent},
  {path: ':id', component: ProjectEditComponent}
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
export class ManageRoutingModule { }
