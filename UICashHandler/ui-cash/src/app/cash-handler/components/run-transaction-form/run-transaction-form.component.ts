import { Component, OnInit } from '@angular/core';
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
  form: any;
  defaultVal = 0;
  giveBack: string | null ="error has occurred";
  constructor(private transService :TransactionsService) {
    // f.setValue({
    //   HundredsAmount: this.defaultVal,
    //   FiftiesAmount: this.defaultVal,
    //   TwentiesAmount: this.defaultVal,
    //   TensAmount: this.defaultVal,
    //   FivesAmount: this.defaultVal,
    //   OnesAmount: this.defaultVal,
    //   DollarCoinAmount: this.defaultVal,
    //   HalfDollarAmount: this.defaultVal,
    //   QuartersAmount: this.defaultVal,
    //   DimesAmount: this.defaultVal,
    //   NicklesAmount: this.defaultVal,
    //   PenniesAmount: this.defaultVal,
    // });
  }

  ngOnInit(): void {

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
