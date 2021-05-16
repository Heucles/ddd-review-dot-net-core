using System;

namespace DddInPractice.Logic.SnackMachines
{
    public sealed class SnackPile : ValueObject<SnackPile>
    {

        public static SnackPile Empty = new SnackPile(Snack.None, 0, 0m);
        public Snack Snack { get; }
        public int Quantity { get; }
        public decimal Price { get; }

        private SnackPile() { }

        public SnackPile(Snack snack, int quantity, decimal price) : this()
        {
            if (quantity < 0) throw new InvalidOperationException();
            if (price < 0) throw new InvalidOperationException();
            if (price % 0.01m > 0) throw new InvalidOperationException();

            Snack = snack;
            Quantity = quantity;
            Price = price;
        }

        protected override bool EqualsCore(SnackPile other)
        {
            return Snack == other.Snack && Quantity == other.Quantity && Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Snack.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        // the imutability principle is clearly well represented by the folloing method in which
        // instead of changing the state of the value object, creates a new instance of it with the new values for its state in it
        internal SnackPile SubtractOne()
        {
            return new SnackPile(this.Snack, this.Quantity - 1, this.Price);
        }
    }
}
