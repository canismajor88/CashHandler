import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import {TransactionsService} from "../../../services/transactions/transactions.service";
@Component({
  selector: 'app-run-trasaction-form',
  templateUrl: './run-trasaction-form.component.html',
  styleUrls: ['./run-trasaction-form.component.css']
})
export class RunTrasactionFormComponent implements OnInit {
  defaultValue="0"
  hasSubmitted=false;
  transactionSuccess=false;
  transactionError=false
  giveBack: string | null ="error has occurred";
  constructor(private transService :TransactionsService) { }

  ngOnInit(): void {
  }

  OnInit(f: NgForm): void {
    f.HundredsAmo.setValue();
  }

  runTransaction(f: NgForm) {
    console.log(f.value);
  this.hasSubmitted=true;
 this.transService.runTransaction(f.value).subscribe(x=>{
   this.giveBack=localStorage.getItem('giveBackString')
   this.transactionSuccess=true
   this.transactionError=false
   this.hasSubmitted=false
 }),()=>{
   this.transactionError=true
   this.transactionSuccess=false
     this.hasSubmitted=false
   if(localStorage.getItem('giveBackString')!==undefined||localStorage.getItem('giveBackString')!==null){

     this.giveBack=localStorage.getItem('giveBackString')
   }
 }
  }
}
