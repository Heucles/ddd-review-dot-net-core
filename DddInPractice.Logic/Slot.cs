using System;

namespace DddInPractice.Logic
{
    public class Slot : Entity
    {
        public virtual SnackPile SnackPile { get; set; }
        public virtual SnackMachine2 SnackMachine { get; set; }
        public virtual int Position { get; protected set; }
        public Slot() { }

        public Slot(SnackMachine2 snackMachine, int position)
        : this()
        {

            Position = position;
            SnackMachine = snackMachine;
            SnackPile = new SnackPile(null, 0, 0m);
        }
    }
}
