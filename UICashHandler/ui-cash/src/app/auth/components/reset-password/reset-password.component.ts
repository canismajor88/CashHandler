import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  constructor(private cashHandlerApi : CashHandlerAuthService ) { }

  ngOnInit(): void {
  }

  resetEmail(f: NgForm) {
    this.cashHandlerApi.ResetPassword(f.value).subscribe(
      x=>{
        console.log("Check Email For Changed Password")
        console.log(x)
      },
      error => {
        console.log(error)
      },
    )
  }
}
