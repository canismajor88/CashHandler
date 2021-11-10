import { Component, OnInit } from '@angular/core';
import {CashHandlerAuthService} from "../../../services/cash-handler-auth/cash-handler-auth.service";
import {MoneyAmountService} from "../../../services/money-amount/money-amount.service";
import {NavigationEnd, Router} from "@angular/router";
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
    let token=  localStorage.getItem('token')
    if(token==""||token==null) this.router.navigate(['/login']);
    console.log("init")

    this.populatePage()
  }
  reLoadpage(){
    window.location.reload();
  }
  populatePage(){

    this.transactionService.getTransactions().subscribe(()=>{
      this.transactionsLoaded=true
    },() =>
    {
      //if error try again
      this.transactionService.getTransactions().subscribe(()=>{
        this.transactionsLoaded=true
      },error =>
      {
        this.transactionsLoaded=false
        console.log(error)
      })
    })
    this.moneyAmountService.getMoneyAmount().subscribe(()=>{
      this.moneyAmountLoaded=true
    },()=>{
      //if error try again
      this.moneyAmountService.getMoneyAmount().subscribe(()=>{
        this.moneyAmountLoaded=true
      },error=>{
        this.router.routeReuseStrategy.shouldReuseRoute = function(){
          return false;
        };
        this.router.events.subscribe((evt) => {
          if (evt instanceof NavigationEnd) {
            // trick the Router into believing it's last link wasn't previously loaded
            this.router.navigated = false;
            // if you need to scroll back to top, here is the right place
            window.scrollTo(0, 0);
          }
        });
        this.moneyAmountLoaded=false
        console.log(error)
      })
    })



  }
}
