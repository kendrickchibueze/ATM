using ATM.ATM_UI;
using ATM_DAL.Data;
using ATM_DAL.Entities;
using ATM_DAL.Services;

namespace ATM
{
    public class Program
    {


        static async Task Main(string[] args)
        {
            await Client.WelcomeMethod();
           
        }



    }
}