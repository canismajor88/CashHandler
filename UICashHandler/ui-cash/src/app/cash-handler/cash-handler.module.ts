import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CashHandlerRoutingModule } from './cash-handler-routing.module';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { AddTransactionComponent } from './components/add-transaction/add-transaction.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { RebalanceComponent } from './components/rebalance/rebalance.component';
import { InternalNavComponent } from './components/internal-nav/internal-nav.component';
import { MoneyAmountsDisplayComponent } from './components/money-amounts-display/money-amounts-display.component';
import { TransactionTableComponent } from './components/transaction-table/transaction-table.component';



@NgModule({
  declarations: [
    TransactionsComponent,
    AddTransactionComponent,
    UserProfileComponent,
    RebalanceComponent,
    InternalNavComponent,
    MoneyAmountsDisplayComponent,
    TransactionTableComponent
  ],
  imports: [
    CommonModule,
    CashHandlerRoutingModule
  ],
  exports: [
  ]
})
export class CashHandlerModule { }
