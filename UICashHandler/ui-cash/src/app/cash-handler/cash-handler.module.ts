import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserNavBarComponent } from './components/user-nav-bar/user-nav-bar.component';
import { ColumnOneComponent } from './layouts/column-one/column-one.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';



@NgModule({
  declarations: [
    UserNavBarComponent,
    ColumnOneComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ColumnOneComponent
  ]
})
export class CashHandlerModule { }
