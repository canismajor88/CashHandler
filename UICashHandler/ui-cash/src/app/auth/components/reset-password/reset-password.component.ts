import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  passwordReset:boolean=false
  passwordResetError:boolean=false
  passwordSubmitted:boolean=false;
  constructor(private cashHandlerApi : CashHandlerAuthService ) { }

  ngOnInit(): void {
  }

  resetEmail(f: NgForm) {
    this.passwordSubmitted=true;
    this.cashHandlerApi.ResetPassword(f.value).subscribe(
      x=>{
        this.passwordReset=true
        this.passwordResetError=false
        this.passwordSubmitted=false;
        console.log("Check Email For Changed Password")
        console.log(x)
      },
      error => {
        this.passwordResetError=true
        this.passwordReset=false
        this.passwordSubmitted=false;
        console.log(error)
      },
    )
  }
}
