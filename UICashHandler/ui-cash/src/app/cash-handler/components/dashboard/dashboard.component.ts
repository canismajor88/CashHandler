import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  addTransaction!: boolean;
  listTransactions!: boolean;
  rebalance!: boolean;
  userProfile!: boolean;

  constructor() {
  }

  ngOnInit(): void {
    this.addTransaction = false;
    this.listTransactions = true;
    this.rebalance = false;
    this.userProfile = false;
  }

  viewAddTransaction(): void {
    this.addTransaction = true;
    this.listTransactions = false;
    this.rebalance = false;
    this.userProfile = false;
  }

  viewListTransactions(): void {
    this.addTransaction = false;
    this.listTransactions = true;
    this.rebalance = false;
    this.userProfile = false;
  }

  viewRebalance(): void {
    this.addTransaction = false;
    this.listTransactions = false;
    this.rebalance = true;
    this.userProfile = false;
  }

  viewUserProfile(): void {
    this.addTransaction = false;
    this.listTransactions = false;
    this.rebalance = false;
    this.userProfile = true;
  }

}
