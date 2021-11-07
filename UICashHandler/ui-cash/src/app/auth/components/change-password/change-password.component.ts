import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth/cash-handler-auth.service";
import {NgForm} from "@angular/forms";
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  userCreditails:any ={};
  passwordReset:boolean=false;
  passwordResetError:boolean=false;
  passwordSubmitted:boolean=false;
  constructor(private  route: ActivatedRoute,private cashHandlerApi: CashHandlerAuthService) { }

  ngOnInit(): void {
    this.userCreditails.token=this.route.snapshot.queryParamMap.get('token'),
    this.userCreditails.userid=this.route.snapshot.queryParamMap.get('userid')
  }

  changePassword() {
    this.passwordSubmitted=true;
    this.cashHandlerApi.ChangePassword(this.userCreditails).subscribe(x=>{
      this.passwordReset=true;
      this.passwordResetError=false;
      this.passwordSubmitted=false;
        console.log(x)
      },error => {
      this.passwordReset=false;
      this.passwordResetError=true;
      this.passwordSubmitted=false;
        console.log(error)
      }
    )
  }
}
