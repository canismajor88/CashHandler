import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { RegisterComponent } from './components/register/register.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import {AppComponent} from "../app.component";
import { LoginUserComponent } from './components/login-user/login-user.component';
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    RegisterComponent,
    ResetPasswordComponent,
    LoginUserComponent,
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    FormsModule
  ],
  exports:[
    RegisterComponent,
    ResetPasswordComponent,
    LoginUserComponent,
  ],
})
export class AuthModule { }
