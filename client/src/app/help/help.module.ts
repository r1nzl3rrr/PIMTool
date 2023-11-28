import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HelpComponent } from './help.component';
import { HelpRoutingModule } from './help-routing.module';



@NgModule({
  declarations: [
    HelpComponent
  ],
  imports: [
    CommonModule,
    HelpRoutingModule
  ]
})
export class HelpModule { }
