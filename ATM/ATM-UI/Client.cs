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

           input: Utility.PrintColorMessage(ConsoleColor.Cyan, "Enter your card number: ");
            try
            {

            
                Int64 cardNumber = Int64.Parse(Console.ReadLine());

                Utility.PrintColorMessage(ConsoleColor.Cyan, "Enter your PIN code: ");

                int pinCode = Int32.Parse(Console.ReadLine());

                try
                {
                    using (var dbContext = new AtmDbContextFactory().CreateDbContext(null))
                    {
                        await atmService.SeedDataAsync();

                        Utility.PrintColorMessage(ConsoleColor.Cyan, "\n\nAuthenticating user...Please wait...");

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
                                        Utility.PrintColorMessage(ConsoleColor.Green, $"Your current balance is: {FormatAmount(balance)}");
                                        await Task.Delay(3000);
                                        break;

                                    case 2:
                                    inputdepo: Utility.PrintColorMessage(ConsoleColor.Cyan, "Enter the amount to deposit: ");
                                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                                        if (depositAmount < 0)
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Red, "Error: Cannot deposit a negative amount.");
                                            goto inputdepo;
                                        }
                                        bool depositResult = await atmService.DepositAsync(cardNumber, pinCode, depositAmount);
                                        if (depositResult)
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Green, "Deposit successful.");
                                        }
                                        else
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Red, "Error: Deposit failed.");
                                        }

                                        await Task.Delay(3000);
                                        break;

                                    case 3:
                                    inputwithdraw: Utility.PrintColorMessage(ConsoleColor.Cyan, "Enter the amount to withdraw: ");

                                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());

                                        if (withdrawAmount < 0)
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Red, "Error: Cannot withdraw a negative amount.");
                                            goto inputwithdraw;
                                        }
                                        bool withdrawResult = await atmService.WithdrawAsync(cardNumber, pinCode, withdrawAmount);
                                        if (withdrawResult)
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Green, "Withdrawal successful.");
                                        }
                                        else
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Red, "Error: Withdrawal failed.");
                                        }
                                        await Task.Delay(3000);
                                        break;

                                    case 4:
                                    intransfer: Utility.PrintColorMessage(ConsoleColor.Cyan, "Enter the recipient's Account number: ");

                                        Int64 recipientAccountNumber = Int64.Parse(Console.ReadLine());

                                        Utility.PrintColorMessage(ConsoleColor.Cyan, "Enter the amount to transfer: ");

                                        decimal transferAmount = decimal.Parse(Console.ReadLine());

                                        if (transferAmount < 0)
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Red, "Error: Cannot transfer a negative amount.");
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
                                            Utility.PrintColorMessage(ConsoleColor.Green, "Transfer successful.");
                                        }
                                        else
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Red, "Error: Transfer failed.");
                                        }

                                        await Task.Delay(6000);
                                        break;

                                    case 5:
                                        var transactions = await atmService.ViewAllTransactionsAsync(cardNumber, pinCode);

                                        Utility.PrintColorMessage(ConsoleColor.Cyan, "Transactions:");

                                        foreach (var transaction in transactions)
                                        {
                                            Utility.PrintColorMessage(ConsoleColor.Green, $"- Type: {transaction.TransactionType}, Amount: {FormatAmount(transaction.TransactionAmount)}, Date: {transaction.TransactionDate}");
                                        }
                                        await Task.Delay(4000);

                                        

                                        break;

                                    case 6:
                                        Utility.PrintColorMessage(ConsoleColor.Green, "Logout successful.");

                                        Environment.Exit(0);
                                        break;

                                    default:
                                        Utility.PrintColorMessage(ConsoleColor.Red, "Error: Invalid operation.");
                                        goto input;
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Utility.PrintColorMessage(ConsoleColor.Red, $"Error: {ex.Message}");
                                goto input;
                                
                            }

                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utility.PrintColorMessage(ConsoleColor.Red, $"Error: {ex.Message}");
                    goto input;
                    return;
                }

            }
            catch(Exception ex)
            {
                Utility.PrintColorMessage(ConsoleColor.Red, $"Error: {ex.Message}");
                goto input;
            }

          

     
        }
    }
}
