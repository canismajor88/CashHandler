import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  UserCreated:boolean=false
  CreationError:boolean=false
  UserSubmitted:boolean=false;
  constructor(private apiService : CashHandlerAuthService) { }

  ngOnInit(): void {
  }

  register(f: NgForm) {
    this.UserSubmitted=true
    this.apiService.register(f.value).subscribe(
      x=>{
        this.UserCreated=true;
        this.CreationError=false;
        this.UserSubmitted=false;
        console.log("User created")
      },
      error => {
        console.log(error)
        this.UserSubmitted=false;
        this.CreationError=true;
        this.UserCreated=false;
      },
    )
  }
}
