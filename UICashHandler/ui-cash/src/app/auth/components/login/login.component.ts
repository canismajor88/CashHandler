import { Component, OnInit } from '@angular/core';
import { CashHandlerAuthService } from "../../../services/cash-handler-auth/cash-handler-auth.service";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginSuccess=false;
  loginAttempted=false;
  loginError=false;
  constructor(private router: Router, private apiService : CashHandlerAuthService) { }

  ngOnInit(): void {
  }

  login(f: NgForm) {
    this.loginAttempted=true;
  this.apiService.login(f.value).subscribe(
    x=>{
      console.log()
      this.loginAttempted=false;
      this.loginSuccess=true;
      this.loginError=false;
      this.router.navigate(['/transactions']);
    },
    error => {
      console.log(error)
      this.loginError=true;
      this.loginSuccess=false;
      this.loginAttempted=false
    },
    )
  }
}
