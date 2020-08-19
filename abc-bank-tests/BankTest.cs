using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary_ReportsOnCustomersAndTheirOpenedAccounts()
        {
            Bank bofa = new Bank();
            Customer mark = new Customer("Mark");
            bofa.AddCustomer(mark);
            mark.OpenAccount(new Account(Account.CHECKING));
            mark.OpenAccount(new Account(Account.SAVINGS));

            string report = bofa.CustomerSummary();

            Assert.AreEqual("Customer Summary\n - Mark (2 accounts)", report);
        }

        [TestMethod]
        public void CustomerSummary_PluralizesAccountsToBeGrammaticallyCorrect()
        {
            Bank capfed = new Bank();
            Customer fred = new Customer("Fred");
            capfed.AddCustomer(fred);
            fred.OpenAccount(new Account(Account.CHECKING));
            Customer jane = new Customer("Jane");
            capfed.AddCustomer(jane);
            jane.OpenAccount(new Account(Account.CHECKING));
            jane.OpenAccount(new Account(Account.SAVINGS));

            string report = capfed.CustomerSummary();

            Assert.AreEqual("Customer Summary\n - Fred (1 account)\n - Jane (2 accounts)", report);
        }

        [TestMethod]
        public void CustomerSummary_HasNothingToReportOnWithoutCustomers()
        {
            Bank lonelyBank = new Bank();

            string report = lonelyBank.CustomerSummary();

            Assert.AreEqual("Customer Summary", report);
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
