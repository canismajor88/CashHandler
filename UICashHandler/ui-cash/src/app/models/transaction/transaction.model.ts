import {MoneyAmount} from "../moneyAmount/money-amount.model";

export interface Transaction {
  TransactionId: string;
  Denominations: string;
  Amount: number;
  transactionDate: Date;
}
