import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  PasswordReset:boolean=false
  PasswordResetError:boolean=false
  PasswordSubmitted:boolean=false;
  constructor(private cashHandlerApi : CashHandlerAuthService ) { }

  ngOnInit(): void {
  }

  resetEmail(f: NgForm) {
    this.PasswordSubmitted=true;
    this.cashHandlerApi.ResetPassword(f.value).subscribe(
      x=>{
        this.PasswordReset=true
        this.PasswordResetError=false
        this.PasswordSubmitted=false;
        console.log("Check Email For Changed Password")
        console.log(x)
      },
      error => {
        this.PasswordResetError=true
        this.PasswordReset=false
        this.PasswordSubmitted=false;
        console.log(error)
      },
    )
  }
}
