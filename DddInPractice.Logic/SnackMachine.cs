namespace DddInPractice.Logic
{
    public sealed class SnackMachine
    {
        public Money MoneyInside { get; private set; }
        public Money MoneyInTransaction { get; private set; }
        public void InsertMoney(Money insertedMoney)
        {
            MoneyInTransaction += insertedMoney;
        }

        public void ReturnMoney()
        {
            KillCurrentTransaction();
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            KillCurrentTransaction();

        }

        private void KillCurrentTransaction()
        {
            MoneyInTransaction = null;
        }

    }
}
