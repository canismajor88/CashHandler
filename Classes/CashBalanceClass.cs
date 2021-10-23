using CashHandler.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashHandler.Classes
{
    public class CashBalanceClass
    {
        public IEnumerable<CashBalance> SelectAll()
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            IEnumerable<CashBalance> cashBalance = CashHandler.CashBalances;

            return cashBalance;

        }

        public CashBalance SelectByID(CashBalance cashBalance)
        {
            if (checkExists(cashBalance))
            {
                CashHandlerEntities CashHandler = new CashHandlerEntities();
                CashBalance cashBalanceReturned = CashHandler.CashBalances.Single(cashBalanceSelect => cashBalanceSelect.CashBalanceID == cashBalance.CashBalanceID);
                return cashBalanceReturned;
            }

            return null;
        }

        public void Insert(CashBalance cashBalance)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            CashHandler.CashBalances.Add(cashBalance);
            CashHandler.SaveChanges();

        }
        public void Update(CashBalance cashBalance)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            CashBalance cb = CashHandler.CashBalances.Single(CashBalanceRow => CashBalanceRow.CashBalanceID == cashBalance.CashBalanceID);

            cb = cashBalance;
            CashHandler.SaveChanges();
        }
        public void Delete(CashBalance cashBalance)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.CashBalances.Any(CashBalanceRow => CashBalanceRow.CashBalanceID == cashBalance.CashBalanceID)){
                CashBalance cb = CashHandler.CashBalances.Single(CashBalanceRow => CashBalanceRow.CashBalanceID == cashBalance.CashBalanceID);
                CashHandler.CashBalances.Remove(cb);
                CashHandler.SaveChanges();
            }
        }

        public void DeleteAll(CashBalance cashBalance)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.CashBalances.Any(CashBalanceRow => CashBalanceRow.CashBalanceID == cashBalance.CashBalanceID))
            {
                CashBalance cb = CashHandler.CashBalances.Single(CashBalanceRow => CashBalanceRow.CashBalanceID == cashBalance.CashBalanceID);
                CashHandler.CashBalances.Remove(cb);
                CashHandler.SaveChanges();
            }
        }

        public int getCount()
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            return CashHandler.CashBalances.Count();
        }

        public bool checkExists(CashBalance cashBalance)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.CashBalances.Any(cashBalanceFound => cashBalanceFound.CashBalanceID == cashBalance.CashBalanceID))
            {
                return true;
            }
            return false;
        }

    }
}