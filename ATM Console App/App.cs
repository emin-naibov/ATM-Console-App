using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace ATM_Console_App
{
    static class App
    {
        static List<Account> accounts = new List<Account>();
        static string[] First_Menu { get; set; } = { "Enter", "Exit" };
        static string[] Operations_Menu { get; set; } = { "Withdraw Money", "Add Money", "Transfer Money" };
        static string[] WithdrawMenu { get; set; } = { "Withdraw Azn", "Withdraw Usd", "Withdraw Eur" };
        static string[] AddMenu { get; set; } = { "Add Azn", "Add Usd", "Add Eur" };
        static string[] CurrencyMenu { get; set; } = { "AZN", "USD", "EUR" };


        public static int CardCheck(long card_num)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (card_num == accounts[i].CardNum)
                {
                    return i;
                }
            }
            return -1;
        }
        static void GetAccs()
        {
            try
            {
                string json = File.ReadAllText("akk.json");
                accounts = JsonSerializer.Deserialize<List<Account>>(json);
            }
            catch (Exception)
            {
                Console.WriteLine("Cant upload data from your file!");
                Thread.Sleep(5000);
                Environment.Exit(-1);
            }
        }
        public static void AccountView(int index)
        {
            Console.Clear();
            Console.WriteLine($"Hello, {accounts[index].FullName}");
            Console.WriteLine($"Your balance is: {accounts[index].Balance} {accounts[index].Currency}");
            Console.WriteLine();
            Console.WriteLine();
            int selected_index = Interface.MenuNavigation(Operations_Menu, 0, 4);

        }

        public static bool PinCheck(int id, int pin)
        {
              if (accounts[id].Pin == pin)
              {
                return true;
              }
              else
              {
                return false;
              }
        }
        static void UpdateAccs()
        {
            try
            {
                string json = JsonSerializer.Serialize(accounts);
                File.WriteAllText("akk.json", json);
            }
            catch (Exception)
            {
                Console.WriteLine("Cant update changed data to your file!"); ;
            }
        }

        public static void BalanceAdd(int index, double azn_amount)
        {
            if (accounts[index].Currency == "AZN")
            {
                accounts[index].Balance += azn_amount;
            }
            else if (accounts[index].Currency == "USD")
            {
                double result_amount = AznMoneyOperations.MoneyFromAzn(1, azn_amount);
                accounts[index].Balance += result_amount;

            }
            else
            {
                double result_amount = AznMoneyOperations.MoneyFromAzn(2, azn_amount);
                accounts[index].Balance += result_amount;
            }
        }
    }
}
