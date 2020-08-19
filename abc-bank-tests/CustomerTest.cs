﻿using abc_bank;
using abc_bank.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingsAccount();

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void OpenAccount_AddsToTheCustomersListOfAccounts()
        {
            Customer john = new Customer("John");

            john.OpenAccount(new CheckingAccount());
            john.OpenAccount(new SavingsAccount());

            Assert.AreEqual(2, john.Accounts.Count);
        }

        /// <remarks>
        /// It's a neat part of the implementation that the call returns its Customer instance,
        /// so I figured that behavior should explicitly be documented in a test so it isn't
        /// accidentally lost (or considered unnecessary and removed) in a potential future change.
        /// </remarks>
        [TestMethod]
        public void OpenAccount_MayBeChainedToOpenManyAccounts()
        {
            Customer jake = new Customer("Jake")
                .OpenAccount(new CheckingAccount())
                .OpenAccount(new SavingsAccount())
                .OpenAccount(new SavingsAccount());

            Assert.AreEqual(3, jake.Accounts.Count);
        }

        [TestMethod]
        public void TotalInterestEarned_SumsInterestAcrossCustomersAccounts()
        {
            Customer john = new Customer("John");
            Account checking = new CheckingAccount();
            Account savings = new SavingsAccount();
            john.OpenAccount(checking).OpenAccount(savings);
            checking.Deposit(1000.0);
            savings.Deposit(2000.0);

            double interest = john.TotalInterestEarned();

            // $1000 in checking at 0.01% = 1, plus
            // $2000 in savings (first thousand at 0.01% (1),
            //   second thousand at 0.02% (2)) = 3
            Assert.AreEqual(4.0, interest);
        }
    }
}
