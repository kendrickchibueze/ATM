using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_DAL.Entities
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }

        public Int64 BankAccountNoFrom { get; set; }


        public Int64 BankAccountNoTo { get; set; }

        public TransactionType TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }


        public DateTime TransactionDate { get; set; }


        public BankAccounts FromAccount { get; set; }
        public BankAccounts ToAccount { get; set; }

    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer
    }
}
