using ATM_DAL.Data;
using ATM_DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ATM_DAL.Services
{
    public class Service
    {


        public static async Task<AtmDbContext> GetDbContextAsync()
        {
            AtmDbContextFactory atmDbContextFactory = new AtmDbContextFactory();
            var dbContext = atmDbContextFactory.CreateDbContext(null);
            return await Task.FromResult(dbContext);
        }

        public async Task SeedDataAsync()
        {
            var dbContext = await GetDbContextAsync();
            if (!dbContext.BankAccounts.Any())
            {
                var bankAccounts = new List<BankAccounts>
                {
                    new BankAccounts
                    {
                        FullName = "John Doe",
                        AccountNumber = 12345678,
                        CardNumber = 1234567890123456,
                        PinCode = 1234,
                        Balance = 1000.00M,
                        isLocked = false
                    },
                    new BankAccounts
                    {
                        FullName = "Jane Smith",
                        AccountNumber = 87654321,
                        CardNumber = 2345678901234567,
                        PinCode = 5678,
                        Balance = 2000.00M,
                        isLocked = false
                    }
                };

                dbContext.BankAccounts.AddRange(bankAccounts);
                await dbContext.SaveChangesAsync();
            }

        }





        public async Task<bool> DepositAsync(Int64 cardNumber, int pinCode, decimal amount)
        {
            var dbContext = await GetDbContextAsync();

            var user = await VerifyUserCardAndPinAsync(dbContext, cardNumber, pinCode);

            if (user == null)
            {
                return false;
            }

            if (amount < 0)
            {
                Console.WriteLine("Error: Cannot deposit a negative amount.");
                return false;
            }

            user.Balance += amount;
            var transaction = new Transaction
            {
                BankAccountNoFrom = user.CardNumber,
                TransactionType = TransactionType.Deposit,
                TransactionAmount = amount,
                TransactionDate = DateTime.UtcNow
            };


            dbContext.Transactions.Add(transaction);
            await dbContext.SaveChangesAsync();

            return true;
        }






        public async Task<bool> WithdrawAsync(Int64 cardNumber, int pinCode, decimal amount)
        {
            var dbContext = await GetDbContextAsync();
            var user = await VerifyUserCardAndPinAsync(dbContext, cardNumber, pinCode);

            if (user == null)
            {
                return false;
            }

            if (user.Balance < amount)
            {
                return false;
            }

            user.Balance -= amount;
            var transaction = new Transaction
            {
                BankAccountNoFrom = user.CardNumber,
                TransactionType = TransactionType.Withdrawal,
                TransactionAmount = amount,
                TransactionDate = DateTime.UtcNow
            };

            dbContext.Transactions.Add(transaction);
            await dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<bool> TransferAsync(Int64 cardNumber, int pinCode, VmTransfers transfer)
        {
            var dbContext = await GetDbContextAsync();
            var user = await VerifyUserCardAndPinAsync(dbContext, cardNumber, pinCode);
            if (user == null)
            {
                return false;
            }

            if (user.Balance < transfer.TransferAmount)
            {
                return false;
            }

            var recipient = await dbContext.BankAccounts.FirstOrDefaultAsync(x => x.AccountNumber == transfer.RecipientBankAccountNumber);
            if (recipient == null)
            {
                return false;
            }

            user.Balance -= transfer.TransferAmount;
            recipient.Balance += transfer.TransferAmount;

            var transaction = new Transaction
            {
                BankAccountNoFrom = user.CardNumber,
                BankAccountNoTo = recipient.AccountNumber,
                TransactionType = TransactionType.Transfer,
                TransactionAmount = transfer.TransferAmount,
                TransactionDate = DateTime.UtcNow
            };

            dbContext.Transactions.Add(transaction);
            await dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<decimal> CheckBalanceAsync(Int64 cardNumber, int pinCode)
        {
            var dbContext = await GetDbContextAsync();
            var user = await VerifyUserCardAndPinAsync(dbContext, cardNumber, pinCode);
            if (user == null)
            {
                return 0;
            }

            return user.Balance;
        }

        public async Task<BankAccounts> VerifyUserCardAndPinAsync(AtmDbContext dbContext, Int64 cardNumber, int pinCode)
        {
            var user = await dbContext.BankAccounts.FirstOrDefaultAsync(x => x.CardNumber == cardNumber && x.PinCode == pinCode);
            if (user != null && user.isLocked == false)
            {
                return user;
            }

            return null;
        }




        public async Task<List<Transaction>> ViewAllTransactionsAsync(Int64 cardNumber, int pinCode)
        {
            var dbContext = await GetDbContextAsync();

            var user = await VerifyUserCardAndPinAsync(dbContext, cardNumber, pinCode);
            if (user == null)
            {
                return null;
            }

            var transactions = await dbContext.Transactions
                .Where(x => x.BankAccountNoFrom == user.CardNumber || x.BankAccountNoTo == user.CardNumber)
                .ToListAsync();

            return transactions;
        }

    }
}
