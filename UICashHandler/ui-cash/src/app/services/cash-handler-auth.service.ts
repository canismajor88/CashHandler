import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class CashHandlerAuthService {
  authURl="https://localhost:5001"
  constructor(private httpClient:HttpClient) { }

  public login(userCred:any){
   return  this.httpClient.post(this.authURl+"/login",userCred).pipe(
     map((response:any)=>{
       const user= response;
       if(user.Result.Success){
         console.log(user.Result)
        localStorage.setItem('token',user.Result.Payload);
       }
    })
   )
  }
  public register(userCred:any){
    let headers=new HttpHeaders({
      'confirmEmailURL':'http://localhost:4200/confirm_email'
    })
    let options={headers: headers};
    return  this.httpClient.post(this.authURl+"/register",userCred,options).pipe(
      map((response:any)=>{
        console.log(response.result)
      }));
  }

  public ConfirmEmail(token:any, userId:any){
    let userCred:any;
    userCred={
      token:token,
      userId:userId
    }
    return  this.httpClient.post(this.authURl+"/confirm-email",userCred)
  }


  public ResetPassword(userCred:any){
    let headers=new HttpHeaders({
      'resetPasswordURL':'http://localhost:4200/change-password'
    })
    let options={headers: headers};
    return  this.httpClient.post(this.authURl+"/reset-password",userCred,options).pipe(
      map((response:any)=>{
        console.log(response.result)
      }));
  }

  public ChangePassword(userCred:any){
    return  this.httpClient.post(this.authURl+"/change-password",userCred)
  }
}

