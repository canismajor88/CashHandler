import { Component, OnInit } from '@angular/core';
import {MoneyAmount} from "../../../models/moneyAmount/money-amount.model";

@Component({
  selector: 'app-rebalance',
  templateUrl: './rebalance.component.html',
  styleUrls: ['./rebalance.component.css']
})
export class RebalanceComponent implements OnInit {
  hundreds = "0"
  fifties = "0"
  twenties = "0"
  tens = "0"
  fives = "0"
  ones = "0"
  dollarCoins = "0"
  halfDollars = "0"
  quarters = "0"
  dimes = "0"
  nickles = "0"
  pennies = "0"
  total="0"
  moneyAmounts: any
  constructor() { }

  ngOnInit(): void {
    this.populateMoneyAmounts()
  }
  populateMoneyAmounts() {
    let moneyAmountStr: string | null = localStorage.getItem("moneyAmount");
    if (moneyAmountStr) {
      this.moneyAmounts = JSON.parse(moneyAmountStr) as MoneyAmount;
    }
    this.hundreds = this.moneyAmounts.HundredsAmount;
    this.fifties = this.moneyAmounts.FiftiesAmount;
    this.twenties = this.moneyAmounts.TwentiesAmount;
    this.tens = this.moneyAmounts.TensAmount;
    this.fives = this.moneyAmounts.FivesAmount;
    this.ones = this.moneyAmounts.OnesAmount;
    this.dollarCoins = this.moneyAmounts.DollarCoinAmount;
    this.halfDollars = this.moneyAmounts.HalfDollarAmount;
    this.quarters = this.moneyAmounts.QuartersAmount;
    this.dimes = this.moneyAmounts.DimesAmount
    this.nickles = this.moneyAmounts.NicklesAmount;
    this.pennies = this.moneyAmounts.PenniesAmount;
    this.total=this.moneyAmounts.TotalAmount;

  }
}
