import {MoneyAmount} from "../moneyAmount/money-amount.model";

export class Transaction {
  transactionId!: string;
  userId!: string;
  description?: string;
  amount!: number;
  transactionDate!: Date;
}
