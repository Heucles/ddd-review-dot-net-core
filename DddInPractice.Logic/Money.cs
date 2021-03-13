using System;

namespace DddInPractice.Logic
{
    public class Money : ValueObject<Money>
    {
        public int OneCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCount { get; private set; }
        public int OneDollarCount { get; private set; }
        public int FiveDollarCount { get; private set; }
        public int TwentyDollarCount { get; private set; }

        public Money(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
        {
            if (oneCentCount < 0)
                throw new InvalidOperationException();
            if (tenCentCount < 0)
                throw new InvalidOperationException();
            if (quarterCount < 0)
                throw new InvalidOperationException();
            if (oneDollarCount < 0)
                throw new InvalidOperationException();
            if (fiveDollarCount < 0)
                throw new InvalidOperationException();
            if (twentyDollarCount < 0)
                throw new InvalidOperationException();

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(
             money1.OneCentCount + money2.OneCentCount,
             money1.TenCentCount + money2.TenCentCount,
             money1.QuarterCount + money2.QuarterCount,
             money1.OneDollarCount + money2.OneDollarCount,
             money1.FiveDollarCount + money2.FiveDollarCount,
             money1.TwentyDollarCount + money2.TwentyDollarCount
            );

            return sum;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override bool EqualsCore(Money other)
        {
            return
            this.OneCentCount == other.OneCentCount &&
            this.TenCentCount == other.TenCentCount &&
            this.QuarterCount == other.QuarterCount &&
            this.OneDollarCount == other.OneDollarCount &&
            this.FiveDollarCount == other.FiveDollarCount &&
            this.TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = OneCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ QuarterCount;
                hashCode = (hashCode * 397) ^ OneDollarCount;
                hashCode = (hashCode * 397) ^ FiveDollarCount;
                hashCode = (hashCode * 397) ^ TwentyDollarCount;

                return hashCode;

            }
        }
    }
}
