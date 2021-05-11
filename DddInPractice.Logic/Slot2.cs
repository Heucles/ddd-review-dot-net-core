using System;

namespace DddInPractice.Logic
{
    public class Slot2 : Entity
    {
        public virtual SnackPile SnackPile { get; set; }
        public virtual SnackMachine2 SnackMachine2 { get; set; }
        public virtual int Position { get; protected set; }
        public Slot2() { }

        public Slot2(SnackMachine2 snackMachine, int position)
        : this()
        {

            Position = position;
            SnackMachine2 = snackMachine;
            SnackPile = new SnackPile(null, 0, 0m);
        }
    }
}
