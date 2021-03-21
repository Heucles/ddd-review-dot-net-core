using System;
using System.Linq;

namespace DddInPractice.Logic
{
    public class SnackMachine : Entity
    {
        public virtual Money MoneyInside { get; protected set; } = Money.None;
        public virtual Money MoneyInTransaction { get; protected set; } = Money.None;

        public SnackMachine()
        {
        }

        public virtual void InsertMoney(Money insertedMoney)
        {
            Money[] acceptedCoinsAndNotes = { Money.Cent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };

            if (!acceptedCoinsAndNotes.Contains(insertedMoney))
                throw new InvalidOperationException();

            MoneyInTransaction += insertedMoney;
        }

        public virtual void ReturnMoney()
        {
            MoneyInTransaction = Money.None;
        }

        public virtual void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = Money.None;

        }

    }
}
