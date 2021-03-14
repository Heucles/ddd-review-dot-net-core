using System;

namespace DddInPractice.Logic
{
    public class MoneyAdded : ValueObject<MoneyAdded>
    {

        private static readonly MoneyAdded None = new MoneyAdded(Money.None, MoneyLabel.None);
        public static readonly MoneyAdded Cent = new MoneyAdded(Money.Cent, MoneyLabel.OneCent);
        public static readonly MoneyAdded TenCent = new MoneyAdded(Money.TenCent, MoneyLabel.TenCent);
        public static readonly MoneyAdded Quarter = new MoneyAdded(Money.Quarter, MoneyLabel.Quarter);
        public static readonly MoneyAdded Dollar = new MoneyAdded(Money.Dollar, MoneyLabel.OneDollar);
        public static readonly MoneyAdded FiveDollar = new MoneyAdded(Money.FiveDollar, MoneyLabel.FiveDollar);
        public static readonly MoneyAdded TwentyDollar = new MoneyAdded(Money.TwentyDollar, MoneyLabel.TwentyDollar);

        public Money Money;
        public MoneyLabel Label;

        public MoneyAdded() { }

        public MoneyAdded(
            Money money,
            MoneyLabel label)
        {
            this.Money = money;
            this.Label = label;
        }

        protected override bool EqualsCore(MoneyAdded other)
        {
            return
            this.Money == other.Money &&
            this.Label == other.Label;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = this.Money.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)this.Label;

                return hashCode;

            }
        }
    }
}
