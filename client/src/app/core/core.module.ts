import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HeaderComponent } from './header/header.component';



@NgModule({
  declarations: [
    NavBarComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    NavBarComponent,
    HeaderComponent
  ]
})
export class CoreModule { }
