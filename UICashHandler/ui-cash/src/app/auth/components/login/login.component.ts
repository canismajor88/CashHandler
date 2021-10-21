import { Component, OnInit } from '@angular/core';
import {CashHandlerApiService} from "../../../services/cash-handler-api.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private apiService : CashHandlerApiService) { }

  ngOnInit(): void {
  }
  login(username: HTMLInputElement, password: HTMLInputElement){

    this.apiService.login(username.value,password.value).subscribe((data)=>{
      console.log(data)
    })
  }
}
