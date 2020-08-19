using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Bank
    {
        private List<Customer> Customers { get; }

        public Bank()
        {
            Customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public String CustomerSummary() {
            String summary = "Customer Summary";
            foreach (Customer c in Customers)
                summary += "\n - " + c.Name + " (" + Format(c.Accounts.Count, "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double TotalInterestPaid() {
            double total = 0;
            foreach(Customer c in Customers)
                total += c.TotalInterestEarned();
            return total;
        }
    }
}
