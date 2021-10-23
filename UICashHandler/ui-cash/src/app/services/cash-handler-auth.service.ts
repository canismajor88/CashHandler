import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
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
    return  this.httpClient.post(this.authURl+"/register",userCred).pipe(
      map((response:any)=>{
        const user= response;
        if(user.succeeded){
          localStorage.setItem('token',user.token)
        }
      })
    )
  }
}
