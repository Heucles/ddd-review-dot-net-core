using System;
using System.Collections.Generic;
using System.Linq;

namespace DddInPractice.Logic
{
    public class SnackMachine : AggregateRoot
    {
        public virtual Money MoneyInside { get; protected set; }
        public virtual Money MoneyInTransaction { get; protected set; }

        protected virtual IList<Slot> Slots { get; set; }

        public SnackMachine()
        {
            this.MoneyInside = Money.None;
            this.MoneyInTransaction = Money.None;

            // in this domain model every machine will have 3 slots, so for this scenario
            // they can be initialized here
            this.Slots = new List<Slot>
            {
                new Slot(snackMachine: this, position: 1),
                new Slot(snackMachine: this, position: 2),
                new Slot(snackMachine: this, position: 3),
            };
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

        public virtual void BuySnack(int position)
        {
            Slot purchasedSlot = Slots.Single(x => x.Position == position);
            purchasedSlot.SnackPile = purchasedSlot.SnackPile.SubtractOne();

            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = Money.None;

        }

        public virtual void LoadSnacks(SnackPile snackPile, int position)
        {
            Slot slot = this.GetSlot(position);
            slot.SnackPile = snackPile;
        }

        public SnackPile GetSnackPileInSlot(int position)
        {
            return this.GetSlot(position).SnackPile;
        }

        private Slot GetSlot(int position)
        {
            return this.Slots.Single(x => x.Position == position);
        }
    }
}
