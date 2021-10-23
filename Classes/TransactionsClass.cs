using CashHandler.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashHandler.Classes
{
    public class TransactionsClass
    {
        public IEnumerable<Transaction> SelectAll()
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            IEnumerable<Transaction> Transaction = CashHandler.Transactions;

            return Transaction;

        }

        public Transaction SelectByID(Transaction trans)
        {
            if (checkExists(trans))
            {
                CashHandlerEntities CashHandler = new CashHandlerEntities();
                Transaction TransactionReturned = CashHandler.Transactions.Single(TransactionSelect => TransactionSelect.TransactionID == trans.TransactionID);
                return TransactionReturned;
            }

            return null;
        }

        public void Insert(Transaction trans)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            CashHandler.Transactions.Add(trans);
            CashHandler.SaveChanges();

        }
        public void Update(Transaction trans)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            Transaction t = CashHandler.Transactions.Single(TransactionRow => TransactionRow.TransactionID == trans.TransactionID);

            t = trans;
            CashHandler.SaveChanges();
        }
        public void Delete(Transaction trans)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Transactions.Any(TransactionRow => TransactionRow.TransactionID == trans.TransactionID))
            {
                Transaction t = CashHandler.Transactions.Single(TransactionRow => TransactionRow.TransactionID == trans.TransactionID);
                CashHandler.Transactions.Remove(t);
                CashHandler.SaveChanges();
            }
        }

        public void DeleteAll(Transaction trans)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Transactions.Any(TransactionRow => TransactionRow.TransactionID == trans.TransactionID))
            {
                Transaction t = CashHandler.Transactions.Single(TransactionRow => TransactionRow.TransactionID == trans.TransactionID);
                CashHandler.Transactions.Remove(t);
                CashHandler.SaveChanges();
            }
        }

        public int getCount()
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            return CashHandler.Transactions.Count();
        }

        public bool checkExists(Transaction trans)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Transactions.Any(TransactionFound => TransactionFound.TransactionID == trans.TransactionID))
            {
                return true;
            }
            return false;
        }

    }
}