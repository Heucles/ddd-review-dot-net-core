using System;
using System.Collections.Generic;
using System.Linq;

namespace DddInPractice.Logic
{
    public class SnackMachine : Entity
    {
        public virtual Money MoneyInside { get; protected set; }
        public virtual Money MoneyInTransaction { get; protected set; }

        public virtual IList<Slot> Slots { get; protected set; }

        public SnackMachine()
        {
            this.MoneyInside = Money.None;
            this.MoneyInTransaction = Money.None;

            // in this domain model every machine will have 3 slots, so for this scenario
            // they can be initialized here
            this.Slots = new List<Slot>
            {
                new Slot(snack: null, position: 1, quantity: 0, price: 0, snackMachine: this),
                new Slot(snack: null, position: 2, quantity: 0, price: 0, snackMachine: this),
                new Slot(snack: null, position: 3, quantity: 0, price: 0, snackMachine: this),
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
            purchasedSlot.Quantity--;

            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = Money.None;

        }

        public virtual void LoadSnacks(int position, Snack snack, int quantity, decimal price)
        {
            Slot slot = Slots.Single(x => x.Position == position);
            slot.Snack = snack;
            slot.Quantity = quantity;
            slot.Price = price;
        }

    }
}
