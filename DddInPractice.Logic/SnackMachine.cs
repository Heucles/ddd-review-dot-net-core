using System;
using System.Collections.Generic;
using System.Linq;

namespace DddInPractice.Logic
{
    public class SnackMachine : AggregateRoot
    {
        public virtual Money MoneyInside { get; protected set; }
        public virtual decimal MoneyInTransaction { get; protected set; }

        protected virtual IList<Slot> Slots { get; set; }

        public SnackMachine()
        {
            this.MoneyInside = Money.None;
            this.MoneyInTransaction = 0m;

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

            this.MoneyInTransaction += insertedMoney.Amount;
            this.MoneyInside += insertedMoney;
        }

        public virtual void ReturnMoney()
        {
            Money moneyToReturn = MoneyInside.Allocate(this.MoneyInTransaction);
            this.MoneyInside -= moneyToReturn;
            MoneyInTransaction = 0m;
        }

        public virtual void BuySnack(int position)
        {
            Slot selectedSlot = Slots.Single(x => x.Position == position);

            if (this.MoneyInTransaction < selectedSlot.SnackPile.Price)
            {
                throw new InvalidOperationException();
            }

            selectedSlot.SnackPile = selectedSlot.SnackPile.SubtractOne();

            // returning the change back to the customer
            Money change = this.MoneyInside.Allocate(this.MoneyInTransaction - selectedSlot.SnackPile.Price);

            if (change.Amount < MoneyInTransaction - selectedSlot.SnackPile.Price)
                throw new InvalidOperationException();

            this.MoneyInside -= change;
            MoneyInTransaction = 0;

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

        public void LoadMoney(Money money)
        {
            this.MoneyInside += money;
        }
    }
}
