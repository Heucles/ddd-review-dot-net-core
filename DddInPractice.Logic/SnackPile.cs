using System;

namespace DddInPractice.Logic
{
    public sealed class SnackPile : ValueObject<SnackPile>
    {
        private Snack snack;

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
                int hashCode = snack.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        internal SnackPile SubtractOne()
        {
            return new SnackPile(this.Snack, this.Quantity - 1, this.Price);
        }
    }
}
