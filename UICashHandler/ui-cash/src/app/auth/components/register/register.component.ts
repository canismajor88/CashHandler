import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private apiService : CashHandlerAuthService) { }

  ngOnInit(): void {
  }

  register(f: NgForm) {
    this.apiService.register(f.value).subscribe(
      x=>{
        console.log("User created")
      },
      error => {
        console.log(error)

      },
    )
  }
}
