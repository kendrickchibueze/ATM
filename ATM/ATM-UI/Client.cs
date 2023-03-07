using ATM_DAL.Data;
using ATM_DAL.Entities;
using ATM_DAL.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ATM_UI
{
    partial class Client
    {
        private static CultureInfo _enculture = new CultureInfo("en-US");


        public static string FormatAmount(decimal amt)
        {
            return  string.Format(_enculture, "{0:C2}", amt);
            
        }




        public static async Task RunClient()
        {
            Service atmService = new Service();

           input: Console.WriteLine("Enter your card number: ");
            try
            {

            
                Int64 cardNumber = Int64.Parse(Console.ReadLine());

                Console.WriteLine("Enter your PIN code: ");
                int pinCode = Int32.Parse(Console.ReadLine());

                try
                {
                    using (var dbContext = new AtmDbContextFactory().CreateDbContext(null))
                    {
                        await atmService.SeedDataAsync();

                        Utility.PrintColorMessage(ConsoleColor.Cyan, "Authenticating user...Please wait...");

                        Utility.printDotAnimation(15);

                        var user = await atmService.VerifyUserCardAndPinAsync(dbContext, cardNumber, pinCode);



                        if (user == null)
                        {
                            Utility.PrintColorMessage(ConsoleColor.Red, "Error: Invalid card number or PIN code.");
                            goto input;
                            return;
                        }

                        bool login = false;
                        while (!login)
                        {


                            Screen.ShowMenuTwo();

                            int operation = Int32.Parse(Console.ReadLine());

                            try
                            {
                                switch (operation)
                                {
                                    case 1:
                                        decimal balance = await atmService.CheckBalanceAsync(cardNumber, pinCode);
                                        Console.WriteLine($"Your current balance is: {FormatAmount(balance)}");
                                        await Task.Delay(3000);
                                        break;

                                    case 2:
                                    inputdepo: Console.WriteLine("Enter the amount to deposit: ");
                                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                                        if (depositAmount < 0)
                                        {
                                            Console.WriteLine("Error: Cannot deposit a negative amount.");
                                            goto inputdepo;
                                        }
                                        bool depositResult = await atmService.DepositAsync(cardNumber, pinCode, depositAmount);
                                        if (depositResult)
                                        {
                                            Console.WriteLine("Deposit successful.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error: Deposit failed.");
                                        }

                                        await Task.Delay(3000);
                                        break;

                                    case 3:
                                    inputwithdraw: Console.WriteLine("Enter the amount to withdraw: ");
                                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                                        if (withdrawAmount < 0)
                                        {
                                            Console.WriteLine("Error: Cannot withdraw a negative amount.");
                                            goto inputwithdraw;
                                        }
                                        bool withdrawResult = await atmService.WithdrawAsync(cardNumber, pinCode, withdrawAmount);
                                        if (withdrawResult)
                                        {
                                            Console.WriteLine("Withdrawal successful.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error: Withdrawal failed.");
                                        }
                                        await Task.Delay(3000);
                                        break;

                                    case 4:
                                    intransfer: Console.WriteLine("Enter the recipient's Account number: ");

                                        Int64 recipientAccountNumber = Int64.Parse(Console.ReadLine());

                                        Console.WriteLine("Enter the amount to transfer: ");

                                        decimal transferAmount = decimal.Parse(Console.ReadLine());

                                        if (transferAmount < 0)
                                        {
                                            Console.WriteLine("Error: Cannot transfer a negative amount.");
                                            goto intransfer;
                                        }

                                        VmTransfers transfer = new VmTransfers
                                        {
                                            TransferAmount = transferAmount,
                                            RecipientBankAccountNumber = recipientAccountNumber
                                        };

                                        bool transferResult = await atmService.TransferAsync(cardNumber, pinCode, transfer);
                                        if (transferResult)
                                        {
                                            Console.WriteLine("Transfer successful.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error: Transfer failed.");
                                        }

                                        await Task.Delay(3000);
                                        break;

                                    case 5:
                                        var transactions = await atmService.ViewAllTransactionsAsync(cardNumber, pinCode);

                                        Console.WriteLine("Transactions:");

                                        foreach (var transaction in transactions)
                                        {
                                            Console.WriteLine($"- Type: {transaction.TransactionType}, Amount: {FormatAmount(transaction.TransactionAmount)}, Date: {transaction.TransactionDate}");
                                        }
                                        await Task.Delay(3000);
                                        break;

                                    case 6:
                                        Console.WriteLine("Logout successful.");

                                        Environment.Exit(0);
                                        break;

                                    default:
                                        Console.WriteLine("Error: Invalid operation.");
                                        goto input;
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                goto input;
                                //continue;
                            }

                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    goto input;
                    return;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                goto input;
            }

          

     
        }
    }
}
