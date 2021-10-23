using CashHandler.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashHandler.Classes
{
    public class RegisterClass
    {
        public IEnumerable<Register> SelectAll()
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            IEnumerable<Register> Register = CashHandler.Registers;

            return Register;

        }

        public Register SelectByID(Register register)
        {
            if (checkExists(register))
            {
                CashHandlerEntities CashHandler = new CashHandlerEntities();
                Register RegisterReturned = CashHandler.Registers.Single(RegisterSelect => RegisterSelect.RegisterID == register.RegisterID);
                return RegisterReturned;
            }

            return null;
        }

        public void Insert(Register register)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            CashHandler.Registers.Add(register);
            CashHandler.SaveChanges();

        }
        public void Update(Register register)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();
            Register r = CashHandler.Registers.Single(RegisterRow => RegisterRow.RegisterID == register.RegisterID);

            r = register;
            CashHandler.SaveChanges();
        }
        public void Delete(Register register)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Registers.Any(RegisterRow => RegisterRow.RegisterID == register.RegisterID))
            {
                Register r = CashHandler.Registers.Single(RegisterRow => RegisterRow.RegisterID == register.RegisterID);
                CashHandler.Registers.Remove(r);
                CashHandler.SaveChanges();
            }
        }

        public void DeleteAll(Register register)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Registers.Any(RegisterRow => RegisterRow.RegisterID == register.RegisterID))
            {
                Register r = CashHandler.Registers.Single(RegisterRow => RegisterRow.RegisterID == register.RegisterID);
                CashHandler.Registers.Remove(r);
                CashHandler.SaveChanges();
            }
        }

        public int getCount()
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            return CashHandler.Registers.Count();
        }

        public bool checkExists(Register register)
        {
            CashHandlerEntities CashHandler = new CashHandlerEntities();

            if (CashHandler.Registers.Any(RegisterFound => RegisterFound.RegisterID == register.RegisterID))
            {
                return true;
            }
            return false;
        }

    }
}