using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void Deposit_AddsTransactionIncreasingAccountBalance()
        {
            Account savings = new Account(Account.CHECKING);

            savings.Deposit(2000.0);

            Assert.AreEqual(2000.0, savings.sumTransactions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Deposit_RejectsNegativeAmount()
        {
            Account checking = new Account(Account.CHECKING);
            checking.Deposit(1000.0);

            checking.Deposit(-500.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Deposit_RejectsAmountOfZero()
        {
            Account savings = new Account(Account.SAVINGS);
            savings.Deposit(500.0);

            savings.Deposit(0);
        }

        [TestMethod]
        public void Withdraw_AddsTransactionDecreasingAccountBalance()
        {
            Account savings = new Account(Account.SAVINGS);
            savings.Deposit(500.0);

            savings.Withdraw(250.0);

            Assert.AreEqual(250.0, savings.sumTransactions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Withdraw_RejectsNegativeAmount()
        {
            Account checking = new Account(Account.CHECKING);
            checking.Deposit(250.0);

            checking.Withdraw(-100.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Withdraw_RejectsAmountOfZero()
        {
            Account checking = new Account(Account.CHECKING);
            checking.Deposit(500.0);

            checking.Withdraw(0.0);
        }
    }
}
