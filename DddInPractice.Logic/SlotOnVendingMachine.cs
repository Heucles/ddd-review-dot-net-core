using System;

namespace DddInPractice.Logic
{
    public class SlotOnVendingMachine : Entity
    {
        public virtual SnackPile SnackPile { get; set; }
        public virtual VendingMachine VendingMachine { get; set; }
        public virtual int Position { get; protected set; }
        public SlotOnVendingMachine() { }

        public SlotOnVendingMachine(VendingMachine snackMachine, int position)
        : this()
        {

            Position = position;
            VendingMachine = snackMachine;
            SnackPile = new SnackPile(null, 0, 0m);
        }
    }
}
