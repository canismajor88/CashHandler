import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SplashRoutingModule } from './splash-routing.module';
import { SplashScreenComponent } from './components/splash-screen/splash-screen.component';
import {AuthModule} from "../auth/auth.module";


@NgModule({
  declarations: [
    SplashScreenComponent
  ],
  imports: [
    CommonModule,
    SplashRoutingModule,
    AuthModule,
  ]
})
export class SplashModule { }
