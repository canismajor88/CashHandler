import { Component, OnInit } from '@angular/core';
import {CashHandlerApiServiceService} from "../../../services/cash-handler-api-service.service";
import {UserCred} from "../../interfaces/UserCred";
import {NgForm} from "@angular/forms";


@Component({
  selector: 'app-login-user',
  templateUrl: './login-user.component.html',
  styleUrls: ['./login-user.component.css']
})
export class LoginUserComponent implements OnInit {

  constructor(private apiService:CashHandlerApiServiceService) { }

  ngOnInit(): void {

  }

  login(username: HTMLInputElement, password: HTMLInputElement){

    this.apiService.login(username.value,password.value)
}
}
