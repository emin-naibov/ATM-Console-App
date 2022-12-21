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

        public static void FirstView()
        {
            GetAccs();
            int index = Interface.MenuNavigation(First_Menu, 0, 0);
            if (index == 0)
            {
                LoginView();
            }
            else
            {
                Environment.Exit(0);
            }
        }
        public static void LoginView()
        {
            Console.Clear();
            int ind = 0;
            while (true)
            {
                Console.WriteLine("Enter your card number:");
                bool check = long.TryParse(Console.ReadLine(), out long l_num);
                //long card_num = long.Parse(Console.ReadLine());
                //ind = CardCheck(card_num);
                ind = CardCheck(l_num);
                if (ind != -1)
                {
                    break;
                }
                Interface.ConsoleOperations(1);

            }
            while (true)
            {
                Console.WriteLine("Enter your pin");
                string s_pin = Interface.PassInput();
                bool check = int.TryParse(s_pin, out int i_pin);
                //Console.WriteLine(check);
                //Console.WriteLine(i_pin);
                //Console.ReadLine();
                //int pin = int.Parse(Console.ReadLine());
                //bool result = PinCheck(ind, int.Parse(pin));
                bool result = PinCheck(ind, i_pin);
                if (result)
                {
                    break;
                }
                Interface.ConsoleOperations(2);
            }
            AccountView(ind);
        }

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
            double amount;
            while (true)
            {
                Console.WriteLine("Enter your ammount:");
                // double amount = double.Parse(Console.ReadLine());
                _ = double.TryParse(Console.ReadLine(), out amount);
                if (amount != 0)
                {
                    break;
                }
                Interface.ConsoleOperations(3);
            }
            Console.Clear();
            if (selected_index == 0)
            {
                int withdraw_index = Interface.MenuNavigation(WithdrawMenu, 0, 0);
                double azn_amount = AznMoneyOperations.MoneyToAzn(withdraw_index, amount);
                BalanceWithdrawal(index, azn_amount);
            }
            else if (selected_index == 1)
            {
                int add_index = Interface.MenuNavigation(AddMenu, 0, 0);
                double azn_amount = AznMoneyOperations.MoneyToAzn(add_index, amount);
                BalanceAdd(index, azn_amount);
            }
            else
            {
                double tranfer_amount_aze = Transfer(index, amount);
                BalanceWithdrawal(index, tranfer_amount_aze);
            }
            UpdateAccs();
            Console.Clear();
            Console.WriteLine($"Successfully!!!\nYour current balance is: {accounts[index].Balance}{accounts[index].Currency}");
            Thread.Sleep(3000);
            Console.Clear();
            FirstView();

        }
        public static double Transfer(int index, double amount)
        {
            int rec_index = 0;
            while (true)
            {
                Console.WriteLine("Enter recipient card number: ");
                _ = long.TryParse(Console.ReadLine(), out long rec_card_num);
                //long rec_card_num = long.Parse(Console.ReadLine());
                rec_index = CardCheck(rec_card_num);
                if (rec_index != -1)
                {
                    break;
                }
                Interface.ConsoleOperations(1);
            }
            Console.WriteLine("\n\n\nEnter the currency: ");
            int selected_currency = Interface.MenuNavigation(CurrencyMenu, 0, 6);
            double transfer_amount_azn = AznMoneyOperations.MoneyToAzn(selected_currency, amount);
            BalanceAdd(rec_index, transfer_amount_azn);
            return transfer_amount_azn;

        }
        public static void BalanceWithdrawal(int index, double azn_amount)
        {
            if (accounts[index].Currency == "AZN")
            {
                accounts[index].Balance -= azn_amount;
            }
            else if (accounts[index].Currency == "USD")
            {
                double final_amount = AznMoneyOperations.MoneyFromAzn(1, azn_amount);
                accounts[index].Balance -= final_amount;

            }
            else
            {
                double final_amount = AznMoneyOperations.MoneyFromAzn(2, azn_amount);
                accounts[index].Balance -= final_amount;
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
