namespace ATM.ATM_UI
{
    partial class Client
    {
        private static int _choice;
        public static async Task WelcomeMethod()
        {

        inputTry: Utility.PrintColorMessage(ConsoleColor.Cyan, "\nPlease enter an option:");
            try
            {
                Screen.ShowMenuOne();



                _choice = int.Parse(Console.ReadLine());


                switch (_choice)
                {
                    case 1:
                        await RunClient();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    default:
                        Utility.PrintColorMessage(ConsoleColor.Red, "\nInvalid input. Please enter a valid option.");
                        goto inputTry;
                        break;
                }
            }
            catch (FormatException)
            {
                Utility.PrintColorMessage(ConsoleColor.Red, "\nInvalid input format. Please enter a valid integer option.");
                goto inputTry;
            }
            catch (Exception ex)
            {
                Utility.PrintColorMessage(ConsoleColor.Red, $"\nAn error occurred: {ex.Message}");
                goto inputTry;
            }
        }

    }
}
