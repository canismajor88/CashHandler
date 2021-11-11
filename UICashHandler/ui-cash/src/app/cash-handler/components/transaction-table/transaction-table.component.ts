import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {MoneyAmount} from "../../../models/moneyAmount/money-amount.model";
import {Transaction} from "../../../models/transaction/transaction.model";

@Component({
  selector: 'app-transaction-table',
  templateUrl: './transaction-table.component.html',
  styleUrls: ['./transaction-table.component.css']
})
export class TransactionTableComponent implements OnInit {
 transactions: any
  constructor(private router: Router) { }

  ngOnInit(): void {
    let token = localStorage.getItem('token');
    if (token == "" || token == null) this.router.navigate(['/login']);
    if (localStorage.getItem('moneyAmount') != null)
      this.populateTransactions();
  }
  populateTransactions(){
    let TransactionsStr: string | null = localStorage.getItem("transactions");
    if (TransactionsStr) {
      this.transactions = JSON.parse(TransactionsStr) as Transaction ;
    }
  }
  parseDateTime(dateboi: string){
    //@ts-ignore
    let date = new Date(dateboi);
   return (date);
  }
}
