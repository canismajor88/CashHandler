import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SplashScreenComponent} from "./components/splash-screen/splash-screen.component";

const routes: Routes = [
  {path: 'splash',component:SplashScreenComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SplashRoutingModule { }
