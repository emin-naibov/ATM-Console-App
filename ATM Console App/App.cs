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
    }
}
