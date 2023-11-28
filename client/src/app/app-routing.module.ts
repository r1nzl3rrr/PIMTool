import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'manage', loadChildren: () => import('./manage/manage.module').then(m => m.ManageModule)},
  {path: 'new', loadChildren: () => import('./new/new.module').then(m => m.NewModule)},
  {path: 'help', loadChildren: () => import('./help/help.module').then(m => m.HelpModule)},
  {path: '**', redirectTo: 'manage', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
