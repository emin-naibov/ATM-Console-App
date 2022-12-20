namespace ATM_Console_App
{
    public static class Currency
    {
        public static double Azn_Usd_dif { get; set; } = 1.7;
        public static double Azn_Eur_dif { get; set; } = 1.9;

        public static double Azn_to_Usd(double amount)
        {
            return amount / Azn_Usd_dif;
        }
        public static double Usd_to_Azn(double amount)
        {
            return amount * Azn_Usd_dif;
        }
        public static double Azn_to_Eur(double amount)
        {
            return amount / Azn_Eur_dif;
        }
        public static double Eur_to_Azn(double amount)
        {
            return amount * Azn_Eur_dif;
        }
    }
}
