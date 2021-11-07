import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AddTransactionComponent} from "./components/add-transaction/add-transaction.component";
import {RebalanceComponent} from "./components/rebalance/rebalance.component";
import { TransactionsComponent } from "./components/transactions/transactions.component";
import {UserProfileComponent} from "./components/user-profile/user-profile.component";

const routes: Routes = [
  { path: 'transactions', component: TransactionsComponent },
  {path: 'add-transaction', component:AddTransactionComponent},
  {path: 'rebalance', component:RebalanceComponent},
  {path: 'user-profile', component:UserProfileComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CashHandlerRoutingModule { }
