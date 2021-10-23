import { Component, OnInit } from '@angular/core';
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginSuccess=false;
  loginAttempted=false;
  constructor(private apiService : CashHandlerAuthService) { }

  ngOnInit(): void {
  }

  login(f: NgForm) {
    this.loginAttempted=true;
  this.apiService.login(f.value).subscribe(
    x=>{
      console.log()
      this.loginSuccess=true;
    },
    error => {
      console.log(error)
      this.loginSuccess=false;
    },
    )
  }
}
