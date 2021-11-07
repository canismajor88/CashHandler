import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

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

  constructor(private router: Router) {
  }

  ngOnInit(): void {
    // if user token is invalid, route them back to login
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

  logout(): void {
    localStorage.setItem('token', "");
    this.router.navigate(['/splash']);
  }

}
