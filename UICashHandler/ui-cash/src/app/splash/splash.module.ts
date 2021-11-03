import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SplashRoutingModule } from './splash-routing.module';
import { SplashScreenComponent } from './components/splash-screen/splash-screen.component';
import {AuthModule} from "../auth/auth.module";
import { NavBarComponent } from './components/nav-bar/nav-bar.component';


@NgModule({
  declarations: [
    SplashScreenComponent,
    NavBarComponent
  ],
  exports: [
    NavBarComponent
  ],
  imports: [
    CommonModule,
    SplashRoutingModule,
  ]
})
export class SplashModule { }
