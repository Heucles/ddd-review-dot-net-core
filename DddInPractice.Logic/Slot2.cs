using System;

namespace DddInPractice.Logic
{
    public class Slot2 : Entity
    {
        public virtual int Position { get; protected set; }
        public virtual SnackMachine2 SnackMachine2 { get; set; }

        public virtual SnackPile SnackPile { get; set; }
        public Slot2() { }

        public Slot2(SnackMachine2 snackMachine2, int position)
        : this()
        {

            Position = position;
            SnackMachine2 = snackMachine2;
            SnackPile = new SnackPile(null, 0, 0m);
        }
    }
}
