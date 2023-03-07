using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_DAL.Entities
{
    public class BankAccounts : BaseEntity
    {

        public int BankAccountId { get; set; }
        public string FullName { get; set; }
        public int AccountNumber { get; set; }
        public Int64 CardNumber { get; set; }
        public int PinCode { get; set; }
        public decimal Balance { get; set; }

        public bool isLocked { get; set; }




        public IEnumerable<Transaction> FromTransactions { get; set; }
        public IEnumerable<Transaction> ToTransactions { get; set; }

    }
}
