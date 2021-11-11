import {Component, OnInit, ViewChild} from '@angular/core';
import { NgForm } from "@angular/forms";
import { TransactionsService } from "../../../services/transactions/transactions.service";



@Component({
  selector: 'app-run-transaction-form',
  templateUrl: './run-transaction-form.component.html',
  styleUrls: ['./run-transaction-form.component.css']
})


export class RunTransactionFormComponent implements OnInit {
  hasSubmitted=false;
  transactionSuccess=false;
  transactionError=false;
  defaultVal = 0;
  totalCashIn = 0;
  transaction = {
    hundredsAmount: this.defaultVal,
    fiftiesAmount: this.defaultVal,
    twentiesAmount: this.defaultVal,
    tensAmount: this.defaultVal,
    fivesAmount: this.defaultVal,
    onesAmount: this.defaultVal,
    dollarCoinAmount: this.defaultVal,
    halfDollarsAmount: this.defaultVal,
    quartersAmount: this.defaultVal,
    dimesAmount: this.defaultVal,
    nickelsAmount: this.defaultVal,
    penniesAmount: this.defaultVal,
    description: "",
  };

  giveBack: string | null ="error has occurred";
  constructor(private transService :TransactionsService) {
  }

  ngOnInit(): void {
  }

  convertToCurrency(amount: number) {
    return amount.toLocaleString("en-US", {
      style: "currency",
      currency: "USD",
      minimumFractionDigits: 2,
    });
  }

  getHundredsValue() {
    return this.convertToCurrency(this.transaction.hundredsAmount * 100);
  }

  getFiftiesValue() {
    return this.convertToCurrency(this.transaction.fiftiesAmount * 50);
  }
  getTwentiesValue() {
    return this.convertToCurrency(this.transaction.twentiesAmount * 20);
  }
  getTensValue() {
    return this.convertToCurrency(this.transaction.tensAmount * 10);
  }
  getFivesValue() {
    return this.convertToCurrency(this.transaction.fivesAmount * 5);
  }
  getOnesValue() {
    return this.convertToCurrency(this.transaction.onesAmount * 1);
  }
  getHalfDollarsValue() {
    return this.convertToCurrency(this.transaction.halfDollarsAmount * 0.5);
  }
  getQuartersValue() {
    return this.convertToCurrency(this.transaction.quartersAmount * 0.25);
  }
  getDimesValue() {
    return this.convertToCurrency(this.transaction.dimesAmount * 0.10);
  }
  getNickelsValue() {
    return this.convertToCurrency(this.transaction.nickelsAmount * 0.05);
  }
  getPenniesValue() {
    return this.convertToCurrency(this.transaction.penniesAmount * 0.01);
  }

  runTransaction(f: NgForm) {
    this.hasSubmitted=true;
    this.transService.runTransaction(f.value)
      .subscribe(x=>{
        this.giveBack=localStorage.getItem('giveBackString')
        this.transactionSuccess=true
        this.transactionError=false
        this.hasSubmitted=false
      }),
      () => {
        this.transactionError=true
        this.transactionSuccess=false
        this.hasSubmitted=false
        if(localStorage.getItem('giveBackString')!==undefined||localStorage.getItem('giveBackString')!==null){
          this.giveBack=localStorage.getItem('giveBackString')}
    }
  }
}
