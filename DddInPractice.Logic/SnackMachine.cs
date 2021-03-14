using System;
using System.Linq;

namespace DddInPractice.Logic
{
    public sealed class SnackMachine : Entity
    {
        public Money MoneyInside { get; private set; } = Money.None;
        public Money MoneyInTransaction { get; private set; } = Money.None;
        public void InsertMoney(Money insertedMoney)
        {
            Money[] acceptedCoinsAndNotes = { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDolar };

            if (!acceptedCoinsAndNotes.Contains(insertedMoney))
                throw new InvalidOperationException();

            MoneyInTransaction += insertedMoney;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = Money.None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = Money.None;

        }

    }
}
