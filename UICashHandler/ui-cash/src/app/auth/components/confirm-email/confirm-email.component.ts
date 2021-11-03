import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CashHandlerAuthService} from "../../../services/cash-handler-auth.service";

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {
  confirmed:boolean=false
  token!:string | null
  userId!:string | null
  submitted:boolean=false;
  constructor(private  route: ActivatedRoute,private cashHandlerApi: CashHandlerAuthService) { }

  ngOnInit(): void {
    this.token=this.route.snapshot.queryParamMap.get('token')
    this.userId=this.route.snapshot.queryParamMap.get('userid')
    this.confirmEmail()
  }
  confirmEmail(){
    this.submitted=true;
  this.cashHandlerApi.ConfirmEmail(this.token,this.userId).subscribe(x=>{
    console.log(x)
    this.submitted=false
    this.confirmed=true;
    },error => {
    console.log(error)
    this.submitted=false;
    this.confirmed=false;
    }
  )

  }
}
