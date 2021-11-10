import { Component, OnInit } from '@angular/core';
import {CashHandlerAuthService} from "../../../services/cash-handler-auth/cash-handler-auth.service";
import {MoneyAmountService} from "../../../services/money-amount/money-amount.service";
import {Router} from "@angular/router";
import {TransactionsService} from "../../../services/transactions/transactions.service";

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit {
   moneyAmountLoaded=false
  transactionsLoaded=false

  constructor(private moneyAmountService : MoneyAmountService, private router: Router, private  transactionService: TransactionsService) {
    let token=  localStorage.getItem('token')
    if(token==""||token==null) this.router.navigate(['/login']);
    this.populatePage()
  }

  ngOnInit(): void {

  }

  populatePage(){
    this.moneyAmountService.getMoneyAmount().subscribe(
      x=>{
        this.moneyAmountLoaded=true;
        this.transactionService.getTransactions().subscribe(
          x=>{
            this.transactionsLoaded=true
          },error => {
            console.log(error)
          }
        )
      },error => {
        console.log(error)
      }
    )


  }
}
