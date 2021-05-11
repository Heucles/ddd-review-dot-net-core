using System;

namespace DddInPractice.Logic
{
    public class Slot : Entity
    {
        public virtual SnackPile SnackPile { get; set; }
        public virtual SnackMachine SnackMachine { get; set; }
        public virtual int Position { get; protected set; }
        public Slot() { }

        public Slot(SnackMachine snackMachine, int position)
        : this()
        {

            Position = position;
            SnackMachine = snackMachine;
            SnackPile = SnackPile.Empty;
        }
    }
}
