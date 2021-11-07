import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from "./auth/auth.module";
import { SplashModule } from "./splash/splash.module";
import { CashHandlerModule } from "./cash-handler/cash-handler.module";
import { MoneyAmountService } from "./services/money-amount/money-amount.service";
import { TransactionsService } from "./services/transactions/transactions.service";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthModule,
    SplashModule,
    CashHandlerModule
  ],
  providers: [TransactionsService, MoneyAmountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
