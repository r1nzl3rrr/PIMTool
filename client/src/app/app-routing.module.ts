import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';

const routes: Routes = [
  {path: 'manage', loadChildren: () => import('./manage/manage.module').then(m => m.ManageModule)},
  {path: 'new', loadChildren: () => import('./new/new.module').then(m => m.NewModule)},
  {path: 'help', loadChildren: () => import('./help/help.module').then(m => m.HelpModule)},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', redirectTo: 'manage', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
