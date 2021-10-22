import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CashHandlerAuthService {

  constructor(private httpClient:HttpClient) { }
  public login(username:string,password:string){
   return  this.httpClient.post("https://localhost:5001/login",{Username:username, Password:password})
  }
}
