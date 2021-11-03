import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";
import {NgForm} from "@angular/forms";
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  userCreditails:any ={};
  PasswordReset:boolean=false;
  PasswordResetError:boolean=false;
  PasswordSubmitted:boolean=false;
  constructor(private  route: ActivatedRoute,private cashHandlerApi: CashHandlerAuthService) { }

  ngOnInit(): void {
    this.userCreditails.token=this.route.snapshot.queryParamMap.get('token'),
    this.userCreditails.userid=this.route.snapshot.queryParamMap.get('userid')
  }

  changePassword() {
    this.PasswordSubmitted=true;
    this.cashHandlerApi.ChangePassword(this.userCreditails).subscribe(x=>{
      this.PasswordReset=true;
      this.PasswordResetError=false;
      this.PasswordSubmitted=false;
        console.log(x)
      },error => {
      this.PasswordReset=false;
      this.PasswordResetError=true;
      this.PasswordSubmitted=false;
        console.log(error)
      }
    )
  }
}
