import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserCred} from "../auth/interfaces/UserCred";

@Injectable({
  providedIn: 'root'
})
export class CashHandlerApiServiceService {

  constructor(private httpClient: HttpClient) { }

 public login(username:string,password:string){

   return  this.httpClient.get("https://localhost:5001/_health");
  }
}
