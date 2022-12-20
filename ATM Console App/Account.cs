namespace ATM_Console_App
{
    class Account
    {
        public long CardNum { get; set; }
        public int Pin { get; set; }
        public string FullName { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }

        public override string ToString()
        {
            return $"{CardNum},{Pin},{FullName},{Balance},{Currency}";
        }
    }
}
