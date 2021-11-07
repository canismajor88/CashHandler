import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {catchError, map} from "rxjs/operators";
import {Observable, throwError} from "rxjs";
import {Transaction} from "../../models/transaction/transaction.model";


const httpOptions = {headers: new HttpHeaders()
  .set('Content-Type', 'application/json')
  .set('authorization', localStorage.token)};

const apiUrl = "https://localhost:5001";

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  constructor(private http: HttpClient) { }

  getTransactions(): Observable<any> {
    const url = `${apiUrl}/transactions`;
    return this.http.get(url, httpOptions).pipe(
      map((response:any)=>{
          const transactions = response;
          if(transactions.Result.Success){
            console.log(transactions.Result)
            localStorage.setItem('transactions',transactions.Result.Payload);
          }
        }),
      catchError(this.handleError));
  }

  getTransaction(id: string): Observable<any> {
    const url = `${apiUrl}/${id}`;
    return this.http.get(url, httpOptions).pipe(
      map((response:any)=>{
        const transaction = response;
        if(transaction.Result.Success){
          console.log(transaction.Result)
          localStorage.setItem('transaction',transaction.Result.Payload);
        }
      }),
      catchError(this.handleError));
  }

  postTransaction(data: Transaction): Observable<any> {
    const url = `${apiUrl}/addTransaction`;
    return this.http.post(url, data, httpOptions)
      .pipe(
        map((response:any)=>{
          const transaction = response;
          if(transaction.Result.Success){
            console.log(transaction.Result)
            localStorage.setItem('transaction',transaction.Result.Payload);
          }
        }),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError('Something bad happened; please try again later.');
  }
}
