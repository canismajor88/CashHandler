import { Component, OnInit } from '@angular/core';
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private apiService : CashHandlerAuthService) { }

  ngOnInit(): void {
  }

  login(f: NgForm) {

  }
}
