﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace abc_bank
{
    public class Customer
    {
        public string Name { get; }
        public List<Account> Accounts { get; } = new List<Account>();

        public Customer(String name)
        {
            Name = name;
        }

        public Customer OpenAccount(Account account)
        {
            Accounts.Add(account);
            return this;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in Accounts)
                total += a.InterestEarned;
            return total;
        }

        public String GetStatement() 
        {
            String statement = "Statement for " + Name + "\n";
            double total = 0.0;
            foreach (Account a in Accounts)
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.Balance;
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String StatementForAccount(Account a)
        {
            String s = "";

            s += $"{a.Name}\n";

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.Transactions) {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double amount) =>
            Math.Abs(amount).ToString("C", CultureInfo.GetCultureInfo("en-US"));
    }
}
