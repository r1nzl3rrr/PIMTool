import { NgModule } from '@angular/core';
import { ManageComponent } from './manage.component';
import { RouterModule, Routes } from '@angular/router';

const routes : Routes = [
  {path: '', component: ManageComponent}
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
