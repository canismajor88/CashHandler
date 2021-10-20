import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {RegisterComponent} from "./components/register/register.component";
import {ResetPasswordComponent} from "./components/reset-password/reset-password.component";
import {LoginUserComponent} from "./components/login-user/login-user.component";

const routes: Routes = [
  {path:'register',component:RegisterComponent},
  {path:'reset-password',component:ResetPasswordComponent},
  {path:'user-login',component:LoginUserComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
