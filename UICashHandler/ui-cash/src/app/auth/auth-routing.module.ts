import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from "./components/login/login.component";
import { RegisterComponent } from "./components/register/register.component";
import { ResetPasswordComponent } from "./components/reset-password/reset-password.component";
import { ConfirmEmailComponent } from "./components/confirm-email/confirm-email.component";
import { ChangePasswordComponent } from "./components/change-password/change-password.component";

const routes: Routes = [
  {path : 'login' ,component:LoginComponent},
  {path :'register', component:RegisterComponent},
  {path :'reset-password',component:ResetPasswordComponent},
  {path :'change-password',component:ChangePasswordComponent},
  {path :'confirm_email',component:ConfirmEmailComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
