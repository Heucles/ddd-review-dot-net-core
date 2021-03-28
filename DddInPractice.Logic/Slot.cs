using System;

namespace DddInPractice.Logic
{
    public class Slot : Entity
    {
        public virtual Snack Snack { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal Price { get; set; }

        public virtual SnackMachine SnackMachine { get; set; }

        public virtual int Position { get; protected set; }

        public Slot() { }

        public Slot(Snack snack, int position, int quantity, decimal price, SnackMachine snackMachine)
        : this()
        {
            Snack = snack;
            Position = position;
            Quantity = quantity;
            Price = price;
            SnackMachine = snackMachine;
        }
    }
}
