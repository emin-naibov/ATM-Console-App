namespace ATM_Console_App
{
    public static class AznMoneyOperations
    {
        public static double MoneyToAzn(int index, double amount)
        {
            double azn_amount = amount;
            switch (index)
            {
                case 0:
                    return amount;
                case 1:
                    azn_amount = Currency.Usd_to_Azn(amount);
                    return azn_amount;
                case 2:
                    azn_amount = Currency.Eur_to_Azn(amount);
                    return azn_amount;
                default:
                    return 0;
            }
        }
        public static double MoneyFromAzn(int index, double amount)
        {
            switch (index)
            {
                case 0:
                    return amount;
                case 1:
                    double usd_amount = Currency.Azn_to_Usd(amount);
                    return usd_amount;
                case 2:
                    double eur_amount = Currency.Azn_to_Eur(amount);
                    return eur_amount;
                default:
                    return 0;
            }
        }
    }
}
