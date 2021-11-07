import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CashHandlerRoutingModule } from './cash-handler-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { AddTransactionComponent } from './components/add-transaction/add-transaction.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { RebalanceComponent } from './components/rebalance/rebalance.component';



@NgModule({
  declarations: [
    DashboardComponent,
    TransactionsComponent,
    AddTransactionComponent,
    UserProfileComponent,
    RebalanceComponent
  ],
  imports: [
    CommonModule,
    CashHandlerRoutingModule
  ],
  exports: [
    DashboardComponent,
    TransactionsComponent,
    AddTransactionComponent,
    UserProfileComponent,
    RebalanceComponent
  ]
})
export class CashHandlerModule { }
