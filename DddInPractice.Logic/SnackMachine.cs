namespace DddInPractice.Logic
{
    public sealed class SnackMachine
    {
        public int OneCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCount { get; private set; }
        public int FiveDollarCount { get; private set; }
        public int TwentyDollarCount { get; private set; }

        public int OneCentCountInTransaction { get; private set; }
        public int TenCentCountInTransaction { get; private set; }
        public int QuarterCountInTransaction { get; private set; }
        public int FiveDollarCountInTransaction { get; private set; }
        public int TwentyDollarCountInTransaction { get; private set; }

        public void InsertMoney(int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            OneCentCountInTransaction += OneCentCount;
            TenCentCountInTransaction += TenCentCount;
            QuarterCountInTransaction += QuarterCount;
            FiveDollarCountInTransaction += FiveDollarCount;
            TwentyDollarCountInTransaction += TwentyDollarCount;
        }

        public void ReturnMoney()
        {
            KillCurrentTransaction();
        }

        public void BuySnack()
        {
            OneCentCount = OneCentCountInTransaction;
            TenCentCount = TenCentCountInTransaction;
            QuarterCount = QuarterCountInTransaction;
            FiveDollarCount = FiveDollarCountInTransaction;
            TwentyDollarCount = TwentyDollarCountInTransaction;

            KillCurrentTransaction();

        }

        private void KillCurrentTransaction()
        {
            OneCentCountInTransaction = 0;
            TenCentCountInTransaction = 0;
            QuarterCountInTransaction = 0;
            FiveDollarCountInTransaction = 0;
            TwentyDollarCountInTransaction = 0;
        }

    }
}
