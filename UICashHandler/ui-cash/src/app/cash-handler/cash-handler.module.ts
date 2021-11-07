import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CashHandlerRoutingModule } from './cash-handler-routing.module';
import { UserNavBarComponent } from './components/user-nav-bar/user-nav-bar.component';
import { ColumnOneComponent } from './layouts/column-one/column-one.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { AddTransactionComponent } from './components/add-transaction/add-transaction.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { RebalanceComponent } from './components/rebalance/rebalance.component';



@NgModule({
  declarations: [
    UserNavBarComponent,
    ColumnOneComponent,
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
    DashboardComponent
  ]
})
export class CashHandlerModule { }
