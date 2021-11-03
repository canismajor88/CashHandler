import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  userCreated:boolean=false
  creationError:boolean=false
  userSubmitted:boolean=false;
  constructor(private apiService : CashHandlerAuthService) { }

  ngOnInit(): void {
  }

  register(f: NgForm) {
    this.userSubmitted=true
    this.apiService.register(f.value).subscribe(
      x=>{
        this.userCreated=true;
        this.creationError=false;
        this.userSubmitted=false;
        console.log("User created")
      },
      error => {
        console.log(error)
        this.userSubmitted=false;
        this.creationError=true;
        this.userCreated=false;
      },
    )
  }
}
